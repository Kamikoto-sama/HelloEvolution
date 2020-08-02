using System;
using System.Collections.Generic;
using System.Linq;
using Emulator.Commands;
using Emulator.Interfaces;

namespace Emulator
{
	public class RandomGenomeBuilder: IGenomeBuilder
	{
		private readonly ICommandsCollection availableCommands;
		private readonly EmulationConfig emulationConfig;

		public RandomGenomeBuilder(ICommandsCollection availableCommands, EmulationConfig emulationConfig)
		{
			this.availableCommands = availableCommands;
			this.emulationConfig = emulationConfig;
		}
		
		public IReadOnlyList<Command> Build()
		{
			var random = new Random();
			return Enumerable
				.Range(0, emulationConfig.GenomeSize)
				.Select(_ =>
				{
					var commandIndex = random.Next(0, availableCommands.CommandsCount);
					var commandFactory = availableCommands[commandIndex];
					var subtypeIndex = random.Next(0, commandFactory.SubtypesCount);
					return commandFactory.Create(subtypeIndex);
				})
				.ToList();
		}
	}
}