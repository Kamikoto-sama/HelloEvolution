using System;
using System.Collections.Generic;
using System.Linq;
using EmulationModel.Commands;
using EmulationModel.Interfaces;

namespace EmulationModel.DefaultImplementations
{
	public class DefaultCommandsCollection: ICommandsCollection
	{
		private readonly ICommandFactory[] availableCommands;
		public int CommandsCount { get; private set; }
		public int TotalSubtypesCount { get; private set; }
		public ICommandFactory this[int index] => availableCommands[index];

		public DefaultCommandsCollection(IEnumerable<ICommandFactory> availableCommands)
		{
			CommandsCount = 0;
			TotalSubtypesCount = 0;
			this.availableCommands = availableCommands
				.Select(command =>
				{
					CommandsCount++;
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
					return command.Create(index % command.SubtypesCount);
			}

			var message = $"Max subtype index is {TotalSubtypesCount - 1}, but got {index}";
			throw new IndexOutOfRangeException(message);
		}
	}
}