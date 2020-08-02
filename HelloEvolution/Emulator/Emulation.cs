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
		private readonly Dictionary<WorldObjectTypes, int> iterationsCountSinceLastItemSpawn;

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
			iterationsCountSinceLastItemSpawn = new Dictionary<WorldObjectTypes, int>
			{
				{WorldObjectTypes.Food, 0},
				{WorldObjectTypes.Poison, 0},
			};
		}

		public void Start()
		{
			Bots ??= generationBuilder.CreateInitial();
			while (StatusMonitor.GenerationIterationNumber >= config.GoalGenerationLifeCount)
			{
				Map = mapProvider.GetMap();
				mapFiller.FillItems(Map);
				mapFiller.FillBots(Map, Bots);
				StatusMonitor.GenerationNumber++;
				RunGeneration();
				var survivedBots = Bots.Where(bot => !bot.IsDead).ToArray();
				StatusMonitor.SurvivedBots = survivedBots;
				Bots = generationBuilder.Rebuild(survivedBots);
			}
		}

		private void RunGeneration()
		{
			StatusMonitor.BotsAliveCount = config.GenerationSize;
			while (StatusMonitor.BotsAliveCount > config.ParentsCount)
			{
				if (++StatusMonitor.GenerationIterationNumber >= config.GoalGenerationLifeCount)
					break;
				foreach (var bot in Bots.Where(bot => !bot.IsDead))
				{
					Command command;
					var commandsExecutedCount = 0;
					do
					{
						command = bot.CurrentCommand;
						command.Execute(bot, Map);
						if (++commandsExecutedCount < config.GenomeSize) continue;
						bot.Health = 0;
						break;
					} while (!command.IsFinal);
					
					bot.Health--;
					if (bot.IsDead)
					{
						Map[bot.Position] = new WorldObject(bot.Position, WorldObjectTypes.Empty);
						if (--StatusMonitor.BotsAliveCount <= config.ParentsCount)
							break;
					}
					
					BotStepPerformed?.Invoke(bot);
					if (config.DelayType == DelayTypes.PerEachBotStep)
						Thread.Sleep(config.IterationDelay);
				}

				SpawnItem(WorldObjectTypes.Food);
				SpawnItem(WorldObjectTypes.Poison);
				GenIterationPerformed?.Invoke();
				if (config.DelayType == DelayTypes.PerEachGenIteration)
					Thread.Sleep(config.IterationDelay);
			}
			StatusMonitor.GenIterationsStatistics.Add(StatusMonitor.GenerationIterationNumber);
			StatusMonitor.GenerationIterationNumber = 0;
		}

		private void SpawnItem(WorldObjectTypes objectType)
		{
			iterationsCountSinceLastItemSpawn[objectType]++;
			var iterationsCount = iterationsCountSinceLastItemSpawn[objectType];
			if (iterationsCount < config.ItemSpawnIterationDelay[objectType])
				return;
			iterationsCountSinceLastItemSpawn[objectType] = 0;
			if (Map.ObjectsCounts[objectType] >= config.InitialItemCountInMap[objectType])
				return;
			IWorldObject ObjFactory(Point pos) => new WorldObject(pos, objectType);
			mapFiller.PlaceObject(ObjFactory, 1, Map);
		}
	}
}