using System.Drawing;
using EmulationModel.Interfaces;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.Commands
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
				case WorldObjectType.Poison:
					bot.Health = 0;
					map[obj.Position] = new Empty(obj.Position);
					break;
				case WorldObjectType.Food:
					bot.IncreaseHealthByFood();
					MoveBot(bot, map, obj.Position);
					break;
				case WorldObjectType.Empty:
					MoveBot(bot, map, obj.Position);
					break;
			}
			bot.MoveCommandPointer((int) obj.Type);
		}

		private void MoveBot(Bot bot, WorldMap map, Point newPosition)
		{
			map[bot.Position] = new Empty(bot.Position);
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