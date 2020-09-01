using EmulationModel.Interfaces;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.Commands
{
	public class InteractCommand: DirectionalCommand
	{
		public override bool IsFinal { get; } = true;

		public InteractCommand(int directionIndex): base(directionIndex)
		{
		}

		public override void Execute(Bot bot, WorldMap map)
		{
			var lookingObj = GetObjByBotDirection(bot.Direction, Direction, bot.Position, map);
			switch (lookingObj.Type)
			{
				case WorldObjectType.Food:
					bot.IncreaseHealthByFood();
					map[lookingObj.Position] = new Empty(lookingObj.Position);
					break;
				case WorldObjectType.Poison:
					map[lookingObj.Position] = new Food(lookingObj.Position);
					break;
			}
			bot.MoveCommandPointer((int) lookingObj.Type);
		}
	}

	public class InteractCommandFactory: ICommandFactory
	{
		public int SubtypesCount { get; } = 8;
		public Command Create(int subTypeIndex) => new InteractCommand(subTypeIndex);
	}
}