using Emulator.Interfaces;

namespace Emulator.Commands
{
	public class MoveCommand: Command
	{
		public override bool IsFinal { get; } = true;
		
		
		
		public override void Execute(Bot bot, WorldMap map)
		{
			
		}
	}
	
	public class MoveCommandFactory: ICommandFactory
	{
		public int SubtypesCount { get; } = 8;
		
		public Command Create(int subTypeIndex)
		{
			throw new System.NotImplementedException();
		}
	}
}