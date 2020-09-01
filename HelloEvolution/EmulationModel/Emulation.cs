using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using EmulationModel.Commands;
using EmulationModel.Interfaces;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel
{
	public class Emulation: BackgroundWorker
	{
		private readonly IWorldMapProvider mapProvider;
		private readonly IWorldMapFiller mapFiller;
		private readonly IGenerationBuilder generationBuilder;
		private readonly Dictionary<WorldObjectType, int> iterationsCountSinceLastItemSpawn;
		private readonly object pauseLocker = new object();
		private readonly object restartLocker = new object();
		private readonly EmulationState state;

		public EmulationConfig Config { get; }
		public WorldMap Map { get; private set; }
		public IEnumerable<Bot> Bots { get; private set; }
		public StatusMonitor StatusMonitor { get; private set; }

		public event Action<EmulationStateName> StateChanged;
		public event Action GenIterationPerformed;

		public Emulation(IWorldMapProvider mapProvider,
						IWorldMapFiller mapFiller,
						IGenerationBuilder generationBuilder,
						EmulationConfig config)
		{
			StatusMonitor = new StatusMonitor();
			state = new EmulationState();
			state.Changed += state => StateChanged?.Invoke(state);
			this.mapProvider = mapProvider;
			this.mapFiller = mapFiller;
			this.generationBuilder = generationBuilder;
			Config = config;
			iterationsCountSinceLastItemSpawn = new Dictionary<WorldObjectType, int>
			{
				{WorldObjectType.Food, 0},
				{WorldObjectType.Poison, 0},
			};
		}

		public bool Pause()
		{
			if (state.Pending || state.Name != EmulationStateName.InAction)
				return false;
			state.Change(EmulationStateName.PendedToPause, false);
			return true;
		}

		public bool Continue()
		{
			if (state.Pending && state.Name != EmulationStateName.PendedToRestart || !state.Paused)
				return false;
			lock(pauseLocker)
				Monitor.Pulse(pauseLocker);
			return true;
		}

		public bool Restart()
		{
			if (state.Pending)
				return false;
			switch (state.Name)
			{
				case EmulationStateName.Finished:
					break;
				case EmulationStateName.InAction:
				case EmulationStateName.Paused:
					state.Change(EmulationStateName.PendedToRestart, false);
					if (state.Paused)
						Continue();
					while (IsBusy) ;
					break;
				default:
					return false;
			}

			StatusMonitor = new StatusMonitor();
			state.Change(EmulationStateName.NotInitialized, false);
			Prepare(true);
			return true;
		}

		public bool Init()
		{
			if (IsBusy || state.Pending || state.Name != EmulationStateName.NotInitialized)
				return false;
			RunWorkerAsync(true);
			return true;
		}

		public bool Start()
		{
			if (IsBusy || state.Pending ||
			    state.Name != EmulationStateName.Initialized &&
			    state.Name != EmulationStateName.Finished)
				return false;
			RunWorkerAsync(false);
			return true;
		}

		protected override void OnDoWork(DoWorkEventArgs e)
		{
			if ((bool) e.Argument)
				Prepare(true);
			else
				Run();
		}

		private void Prepare(bool init = false)
		{
			if (init)
				Bots = generationBuilder.CreateInitial();
			Map = mapProvider.GetMap();
			mapFiller.FillItems(Map);
			mapFiller.FillBots(Map, Bots);

			if (init)
				state.Change(EmulationStateName.Initialized);
		}

		private void Run()
		{
			state.Change(EmulationStateName.InAction);
			while (StatusMonitor.GenerationIterationNumber < Config.GenIterationsCountGoal)
			{
				StatusMonitor.GenerationIterationNumber = 0;
				StatusMonitor.GenerationNumber++;
				RunGeneration();
				if (state.Name == EmulationStateName.PendedToRestart)
					return;
				var survivedBots = Bots.Where(bot => !bot.IsDead).ToArray();
				StatusMonitor.SurvivedBots = survivedBots;
				Bots = generationBuilder.Rebuild(survivedBots);
				Prepare();
			}
			state.Change(EmulationStateName.Finished);
		}

		private void RunGeneration()
		{
			StatusMonitor.BotsAliveCount = Config.GenerationSize;
			while (StatusMonitor.BotsAliveCount > Config.ParentsCount)
			{
				if (++StatusMonitor.GenerationIterationNumber >= Config.GenIterationsCountGoal)
					break;
				foreach (var bot in Bots.Where(bot => !bot.IsDead))
				{
					PerformBotAction(bot);
					if (state.Name == EmulationStateName.PendedToRestart)
						return;
					if (StatusMonitor.BotsAliveCount <= Config.ParentsCount)
						break;
				}

				SpawnItem(WorldObjectType.Food);
				SpawnItem(WorldObjectType.Poison);
				GenIterationPerformed?.Invoke();
				if (Config.DelayType == DelayTypes.PerEachGenIteration)
					Thread.Sleep(Config.IterationDelay);
			}
			StatusMonitor.GenIterationsStatistics.Add(StatusMonitor.GenerationIterationNumber);
		}

		private void PerformBotAction(Bot bot)
		{
			Command command;
			var executedCommandsCount = 0;
			do
			{
				if (HandleEmulationState())
					return;

				command = bot.CurrentCommand;
				command.Execute(bot, Map);

				if (++executedCommandsCount < Config.GenomeSize) continue;
				bot.Health = 0;
				break;
			} while (!command.IsFinal);

			bot.Health--;
			if (!bot.IsDead)
				return;
			Map[bot.Position] = new Empty(bot.Position);
			StatusMonitor.BotsAliveCount--;
		}

		private bool HandleEmulationState()
		{
			if (state.Name == EmulationStateName.PendedToPause)
			{
				state.Change(EmulationStateName.Paused);
				lock (pauseLocker)
					Monitor.Wait(pauseLocker);
				state.Change(EmulationStateName.InAction);
			}

			return state.Name == EmulationStateName.PendedToRestart;
		}

		private void SpawnItem(WorldObjectType objectType)
		{
			iterationsCountSinceLastItemSpawn[objectType]++;
			var iterationsCount = iterationsCountSinceLastItemSpawn[objectType];
			if (iterationsCount < Config.ItemSpawnIterationDelay[objectType])
				return;
			iterationsCountSinceLastItemSpawn[objectType] = 0;
			if (Map.PlacedObjectsCounts[objectType] >= Config.InitialItemCountInMap[objectType])
				return;
			IWorldMapObject ObjFactory(Point pos) => WorldObjFactory.GetWorldObj(objectType, pos);
			mapFiller.PlaceObject(ObjFactory, 1, Map);
		}
	}
}