using Emulator.Commands;

namespace Emulator.Interfaces
{
	public interface ICommandsCollection
	{
		int CommandsCount { get; }
		int TotalSubtypesCount { get; }
		Command GetCommandBySubtypeIndex(int index);
		ICommandFactory this[int index] { get; }
	}
}