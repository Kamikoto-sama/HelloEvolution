using System;
using System.Collections.Generic;

namespace Emulator
{
	public class EmulationConfig
	{
		public int GenerationSize { get; set; } = 64;
		public int GenomeSize { get; set; } = 64;
		public int ParentsCount { get; set; } = 8;
		public int EachParentCopiesCount { get; set; } = 7;
		public int MutationsCount { get; set; } = 8;
		
		public int BotInitialHealth { get; set; } = 35;
		public Directions BotInitialDirection { get; set; } = Directions.Up;
		public int BotMaxHealth { get; set; } = 90;
		public int FoodHealthIncrease { get; set; } = 10;

		public string TxtMapFilePath { get; set; } = "src/map.txt";
		public Dictionary<WorldObjectTypes, int> InitialItemCountInMap { get; } =
			new Dictionary<WorldObjectTypes, int>
			{
				{WorldObjectTypes.Food, 50},
				{WorldObjectTypes.Poison, 50},
			};
		public Dictionary<WorldObjectTypes, int> ItemSpawnIterationDelay { get; } =
			new Dictionary<WorldObjectTypes, int>
			{
				{WorldObjectTypes.Food, 5},
				{WorldObjectTypes.Poison, 5},
			};

		public int GoalGenerationLifeCount { get; set; } = 100000;
		public DelayTypes DelayType { get; set; } = DelayTypes.NoDelay;
		public TimeSpan IterationDelay { get; set; } = TimeSpan.FromMilliseconds(500);
	}
}