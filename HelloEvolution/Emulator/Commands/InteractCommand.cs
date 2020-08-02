using Emulator.Interfaces;

namespace Emulator.Commands
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
				case WorldObjectTypes.Food:
					bot.IncreaseHealthByFood();
					break;
				case WorldObjectTypes.Poison:
					map[lookingObj.Position] = new WorldObject(lookingObj.Position, WorldObjectTypes.Food);
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