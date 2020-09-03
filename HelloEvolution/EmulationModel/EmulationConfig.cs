using System;
using System.Collections.Generic;
using System.IO;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel
{
	public class EmulationConfig
	{
		public int GenerationSize { get; set; } = 64;
		public int ParentsCount { get; set; } = 8;
		public int MutatedBotsCount { get; set; } = 8;
		public int GenomeSize { get; set; } = 64;
		public Range MutatedGenesCount { get; set; } = new Range(2, 8);

		public int BotInitialHealth { get; set; } = 35;
		public int BotMaxHealth { get; set; } = 100;
		public int FoodHealthIncrease { get; set; } = 10;

		public string TxtMapFilePath { get; set; } = "src/map.txt";
		public Dictionary<WorldObjectType, int> InitialItemCountInMap { get; } =
			new Dictionary<WorldObjectType, int>
			{
				{WorldObjectType.Food, 100},
				{WorldObjectType.Poison, 50},
			};
		public Dictionary<WorldObjectType, int> ItemSpawnIterationDelay { get; } =
			new Dictionary<WorldObjectType, int>
			{
				{WorldObjectType.Food, 2},
				{WorldObjectType.Poison, 5},
			};

		public int GenIterationsCountGoal { get; set; } = 90;
		public DelayTypes DelayType { get; set; } = DelayTypes.PerEachGenIteration;
		public TimeSpan IterationDelay { get; set; } = TimeSpan.FromMilliseconds(1000);

		public EmulationConfig()
		{
			ValidateParams();
		}

		private void ValidateParams()
		{
			if (ParentsCount > GenerationSize)
				throw new InvalidDataException("Parents count can't be greater than generation size");
			if (MutatedBotsCount > GenerationSize)
				throw new InvalidDataException("Mutated bots count can't be greater than generation size");
			if (GenerationSize % ParentsCount != 0)
				throw new InvalidDataException("GenerationSize % ParentsCount != 0");
		}
	}
}