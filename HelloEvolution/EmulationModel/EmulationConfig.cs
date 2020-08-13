using System;
using System.Collections.Generic;
using System.IO;

namespace EmulationModel
{
	public class EmulationConfig
	{
		public int GenerationSize { get; set; } = 64;
		public int ParentsCount { get; set; } = 8;
		public int MutationsCount { get; set; } = 8;
		public int EachParentCopiesCount { get; set; } = 7;
		public int GenomeSize { get; set; } = 64;
		public Range MutatedGenesCount { get; set; } = new Range(2, 8);

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

		public int GoalGenerationLifeCount { get; set; } = 200000;
		public DelayTypes DelayType { get; set; } = DelayTypes.NoDelay;
		public TimeSpan IterationDelay { get; set; } = TimeSpan.FromMilliseconds(500);

		public EmulationConfig()
		{
			ValidateParams();
		}

		private void ValidateParams()
		{
			if (ParentsCount * EachParentCopiesCount + MutationsCount != GenerationSize)
			{
				var message = "ParentsCount * EachParentCopiesCount + MutationsCount != GenerationSize";
				throw new InvalidDataException(message);
			}

			if (MutatedGenesCount.End.Value < MutatedGenesCount.Start.Value)
			{
				var message = "MutatedGenesCount: Start value cannot be greater than end value";
				throw new InvalidDataException(message);
			}
		}
	}
}