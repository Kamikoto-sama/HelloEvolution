using System;
using System.Collections.Generic;
using System.Linq;
using EmulationModel.Commands;
using EmulationModel.Interfaces;

namespace EmulationModel.DefaultImplementations
{
	public class DefaultGenomeBuilder: IGenomeBuilder
	{
		private readonly ICommandsCollection availableCommands;
		private readonly EmulationConfig config;
		private readonly Random random;

		public DefaultGenomeBuilder(ICommandsCollection availableCommands, EmulationConfig config, Random random)
		{
			this.availableCommands = availableCommands;
			this.config = config;
			this.random = random;
		}

		public IReadOnlyList<Command> Build()
		{
			return Enumerable
				.Range(0, config.GenomeSize)
				.Select(_ => GetRandomSubtypeCommand())
				.ToList();
		}

		public IReadOnlyList<Command> MutateGenome(IReadOnlyList<Command> genome)
		{
			var mutationsRange = config.MutatedGenesCount;
			var genesCountToMutate = random.Next(mutationsRange.MinValue, mutationsRange.MaxValue + 1);
			var genesIndexes = new HashSet<int>(Enumerable
				.Range(0, mutationsRange.MaxValue)
				.Select(_ => random.Next(0, genesCountToMutate)));
			return genome
				.Select((gene, index) =>
					genesIndexes.Contains(index) ? GetRandomSubtypeCommand() : gene)
				.ToArray();
		}

		private Command GetRandomSubtypeCommand()
		{
			var commandIndex = random.Next(0, availableCommands.TotalSubtypesCount);
			return availableCommands.GetCommandBySubtypeIndex(commandIndex);
		}

		private Command GetRandomCommand()
		{
			var commandIndex = random.Next(0, availableCommands.CommandsCount);
			var commandFactory = availableCommands[commandIndex];
			var subtypeIndex = random.Next(0, commandFactory.SubtypesCount);
			return commandFactory.Create(subtypeIndex);
		}
	}
}