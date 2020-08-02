using System;
using System.Collections.Generic;
using System.Linq;
using Emulator.Commands;
using Emulator.Interfaces;

namespace Emulator
{
	public class CommandsCollection: ICommandsCollection
	{
		private readonly ICommandFactory[] availableCommands;
		public int CommandCount { get; private set; }
		public int TotalSubtypesCount { get; private set; }
		public ICommandFactory this[int index] => availableCommands[index];

		public CommandsCollection(IEnumerable<ICommandFactory> availableCommands)
		{
			CommandCount = 0;
			TotalSubtypesCount = 0;
			this.availableCommands = availableCommands
				.Select(command =>
				{
					CommandCount++;
					TotalSubtypesCount += command.SubtypesCount;
					return command;
				})
				.ToArray();
		}
		
		public Command GetCommandBySubtypeIndex(int index)
		{
			var currentSubtypesCount = 0;
			foreach (var command in availableCommands)
			{
				currentSubtypesCount += command.SubtypesCount;
				if (currentSubtypesCount - 1 >= index)
					return command.Create(currentSubtypesCount % command.SubtypesCount);
			}

			var message = $"Max subtype index is {TotalSubtypesCount - 1}, but got {index}";
			throw new IndexOutOfRangeException(message);
		}
	}
}