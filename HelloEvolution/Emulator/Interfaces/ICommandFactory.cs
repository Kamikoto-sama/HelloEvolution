using Emulator.Commands;

namespace Emulator.Interfaces
{
	public interface ICommandFactory
	{
		int SubtypesCount { get; }
		Command Create(int subTypeIndex);
	}
}