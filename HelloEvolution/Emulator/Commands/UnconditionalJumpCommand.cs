using Emulator.Interfaces;

namespace Emulator.Commands
{
	public class UnconditionalJumpCommand: Command
	{
		private readonly int jumpValue;
		public override bool IsFinal { get; } = false;

		public UnconditionalJumpCommand(int jumpValue) => this.jumpValue = jumpValue;

		public override void Execute(Bot bot, WorldMap map) => bot.MoveCommandPointer(jumpValue + 32);
	}
	
	public class UnconditionalJumpCommandFactory: ICommandFactory
	{
		public int SubtypesCount { get; }

		public UnconditionalJumpCommandFactory(EmulationConfig config) => SubtypesCount = config.GenomeSize / 2;

		public Command Create(int subTypeIndex) => new UnconditionalJumpCommand(subTypeIndex);
	}
}