using Emulator.Commands;

namespace Emulator.Interfaces
{
	public interface ICommandsCollection
	{
		int CommandCount { get; }
		int TotalSubtypesCount { get; }
		Command GetCommandBySubtypeIndex(int index);
		ICommandFactory this[int index] { get; }
	}
}