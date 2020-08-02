namespace Emulator.Commands
{
	public abstract class DirectionalCommand: Command
	{
		protected readonly Directions Direction;

		protected DirectionalCommand(int directionIndex) => Direction = (Directions) directionIndex;
	}
}