using Emulator.Interfaces;

namespace Emulator.Commands
{
	public class RotateCommand: Command
	{
		private readonly int rotationIndex;
		public override bool IsFinal { get; } = false;

		public RotateCommand(int rotationIndex)
		{
			this.rotationIndex = rotationIndex;
		}
		
		public override void Execute(Bot bot, WorldMap map)
		{
			bot.Direction = (Directions) (((int) bot.Direction + rotationIndex) % 8);
			bot.MoveCommandPointer(1);
		}
	}
	
	public class RotateCommandFactory: ICommandFactory
	{
		public int SubtypesCount { get; } = 8;
		public Command Create(int subTypeIndex) => new RotateCommand(subTypeIndex);
	}
}