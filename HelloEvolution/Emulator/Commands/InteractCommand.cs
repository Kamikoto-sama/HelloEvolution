using Emulator.Interfaces;

namespace Emulator.Commands
{
	public class InteractCommand
	{
		
	}
	
	public class InteractCommandFactory: ICommandFactory
	{
		public int SubtypesCount { get; }
		public Command Create(int subTypeIndex) => new InteractCommand(subTypeIndex);
	}
}