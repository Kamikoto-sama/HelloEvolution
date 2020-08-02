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
			var objAtNewPosition = GetObjByBotDirection(bot.Direction, Direction, bot.Position, map);
			switch (objAtNewPosition.Type)
			{
				case WorldObjectTypes.Poison:
					bot.Health = 0;
					break;
				case WorldObjectTypes.Food:
					bot.IncreaseHealthByFood();
					MoveBot(bot, map, objAtNewPosition.Position);
					break;
				case WorldObjectTypes.Empty:
					MoveBot(bot, map, objAtNewPosition.Position);
					break;
			}
			bot.MoveCommandPointer((int) objAtNewPosition.Type);
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