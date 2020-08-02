using System.Drawing;
using Emulator.Interfaces;

namespace Emulator.Commands
{
	public class MoveCommand: DirectionalCommand
	{
		public override bool IsFinal { get; } = true;

		public MoveCommand(int index): base(index)
		{
		}

		public override void Execute(Bot bot, WorldMap map)
		{
			var obj = GetObjByBotDirection(bot.Direction, Direction, bot.Position, map);
			switch (obj.Type)
			{
				case WorldObjectTypes.Poison:
					bot.Health = 0;
					map[obj.Position] = new WorldObject(obj.Position, WorldObjectTypes.Empty);
					break;
				case WorldObjectTypes.Food:
					bot.IncreaseHealthByFood();
					MoveBot(bot, map, obj.Position);
					break;
				case WorldObjectTypes.Empty:
					MoveBot(bot, map, obj.Position);
					break;
			}
			bot.MoveCommandPointer((int) obj.Type);
		}

		private void MoveBot(Bot bot, WorldMap map, Point newPosition)
		{
			map[bot.Position] = new WorldObject(bot.Position, WorldObjectTypes.Empty);
			map[newPosition] = bot;
			bot.Position = newPosition;
		}
	}
	
	public class MoveCommandFactory: ICommandFactory
	{
		public int SubtypesCount { get; } = 8;
		
		public Command Create(int subTypeIndex) => new MoveCommand(subTypeIndex);
	}
}