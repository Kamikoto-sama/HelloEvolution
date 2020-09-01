using EmulationModel.Interfaces;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.Commands
{
	public class LookCommand: DirectionalCommand
	{
		public override bool IsFinal { get; } = false;

		public LookCommand(int directionIndex): base(directionIndex){}

		public override void Execute(Bot bot, WorldMap map)
		{
			var lookingObj = GetObjByBotDirection(bot.Direction, Direction, bot.Position, map);
			bot.MoveCommandPointer((int) lookingObj.Type);
		}
	}
	
	public class LookCommandFactory: ICommandFactory
	{
		public int SubtypesCount { get; } = 8;
		public Command Create(int subTypeIndex) => new LookCommand(subTypeIndex);
	}
}