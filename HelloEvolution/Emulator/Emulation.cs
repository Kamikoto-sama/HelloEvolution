using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Emulator.Commands;
using Emulator.Interfaces;

namespace Emulator
{
	public class Emulation
	{
		private readonly IWorldMapProvider mapProvider;
		private readonly IWorldMapFiller mapFiller;
		private readonly IGenerationBuilder generationBuilder;
		private readonly EmulationConfig config;

		public WorldMap Map { get; private set; }
		public IEnumerable<Bot> Bots { get; private set; }
		public StatusMonitor StatusMonitor { get; }

		public event Action<Bot> BotStepPerformed;
		public event Action GenIterationPerformed;

		public Emulation(IWorldMapProvider mapProvider, 
			IWorldMapFiller mapFiller,
			IGenerationBuilder generationBuilder,
			EmulationConfig config,
			StatusMonitor statusMonitor)
		{
			StatusMonitor = statusMonitor;
			this.mapProvider = mapProvider;
			this.mapFiller = mapFiller;
			this.generationBuilder = generationBuilder;
			this.config = config;
		}

		public void Start()
		{
			if (Map == null || Bots == null)
				Prepare();
			while (++StatusMonitor.GenerationNumber <= config.GoalGenerationLifeCount)
			{
				mapFiller.FillBots(Map, Bots);
				Map.CellChanged += AddItem;
				RunGeneration();
				var survivedBots = Bots.Where(bot => !bot.IsDead).ToArray();
				StatusMonitor.SurvivedBots = survivedBots;
				Bots = generationBuilder.Rebuild(survivedBots);
				Map.CellChanged -= AddItem;
				mapFiller.RemoveObjectsFromMap(survivedBots, Map);
			}
		}

		private void Prepare()
		{
			Bots = generationBuilder.CreateInitial();
			Map = mapProvider.GetMap();
			mapFiller.FillItems(Map);
		}

		private void RunGeneration()
		{
			StatusMonitor.BotsAliveCount = config.GenerationSize;
			while (StatusMonitor.BotsAliveCount > config.ParentsCount)
			{
				StatusMonitor.GenerationIterationNumber++;
				foreach (var bot in Bots.Where(bot => !bot.IsDead))
				{
					Command command;
					do
					{
						command = bot.CurrentCommand;
						command.Execute(bot, Map);
					} while (!command.IsFinal);
					
					bot.Health--;
					if (!bot.IsDead) 
						continue;
					Map[bot.Position] = new WorldObject(bot.Position, WorldObjectTypes.Empty);
					StatusMonitor.BotsAliveCount--;
					
					BotStepPerformed?.Invoke(bot);
					if (config.DelayType == DelayTypes.PerEachBotStep)
						Thread.Sleep(config.IterationDelay);
				}

				GenIterationPerformed?.Invoke();
				if (config.DelayType == DelayTypes.PerEachGenIteration)
					Thread.Sleep(config.IterationDelay);
			}
			StatusMonitor.GenIterationsStatistics.Add(StatusMonitor.GenerationIterationNumber);
			StatusMonitor.GenerationIterationNumber = 0;
		}

		private void AddItem(WorldMapChangedEvent eventArgs)
		{
			var currentObjType = Map[eventArgs.Coordinates.X, eventArgs.Coordinates.Y].Type;
			var prevObjType = eventArgs.PreviousCellObject.Type;
			if (prevObjType == currentObjType ||
			    !new[] {WorldObjectTypes.Food, WorldObjectTypes.Poison}.Contains(prevObjType))
				return;
			IWorldObject ObjFactory(Point pos) => new WorldObject(pos, prevObjType);
			mapFiller.PlaceObject(ObjFactory, 1, Map);
		}
	}
}