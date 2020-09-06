using System;
using System.IO;
using EmulationModel.Models;

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
		public ItemsSpawnSettings MaxItemsCountOnMap { get; } = new ItemsSpawnSettings {Food = 100, Poison = 50};
		public ItemsSpawnSettings ItemSpawnIterationDelay { get; } = new ItemsSpawnSettings{Food = 2, Poison = 5};

		public int GenIterationsCountGoal { get; set; } = 90;
		public DelayTypes DelayType { get; set; } = DelayTypes.PerEachGenIteration;
		public double IterationDelayMilliseconds { get; set; } = TimeSpan.FromSeconds(1).TotalMilliseconds;

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