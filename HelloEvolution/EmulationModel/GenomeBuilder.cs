using System;
using System.Collections.Generic;
using System.Linq;
using EmulationModel.Commands;
using EmulationModel.Interfaces;

namespace EmulationModel
{
	public class GenomeBuilder: IGenomeBuilder
	{
		private readonly ICommandsCollection availableCommands;
		private readonly EmulationConfig config;

		public GenomeBuilder(ICommandsCollection availableCommands, EmulationConfig config)
		{
			this.availableCommands = availableCommands;
			this.config = config;
		}
		
		public IReadOnlyList<Command> Build() =>
			Enumerable
				.Range(0, config.GenomeSize)
				.Select(_ => GetRandomSubtypeCommand())
				.ToList();

		public IReadOnlyList<Command> MutateGenome(IReadOnlyList<Command> genome)
		{
			var random = new Random();
			var mutationsRange = config.MutatedGenesCount;
			var genesCountToMutate = random.Next(mutationsRange.Start.Value, mutationsRange.End.Value + 1);
			var genesIndexes = new HashSet<int>(Enumerable
				.Range(0, mutationsRange.End.Value)
				.Select(_ => random.Next(0, genesCountToMutate)));
			return genome
				.Select((gene, index) => 
					genesIndexes.Contains(index) ? GetRandomSubtypeCommand() : gene)
				.ToArray();
		}

		private Command GetRandomSubtypeCommand()
		{
			var random = new Random();
			var commandIndex = random.Next(0, availableCommands.TotalSubtypesCount);
			return availableCommands.GetCommandBySubtypeIndex(commandIndex);
		}

		private Command GetRandomCommand()
		{
			var random = new Random();
			var commandIndex = random.Next(0, availableCommands.CommandsCount);
			var commandFactory = availableCommands[commandIndex];
			var subtypeIndex = random.Next(0, commandFactory.SubtypesCount);
			return commandFactory.Create(subtypeIndex);
		}
	}
}