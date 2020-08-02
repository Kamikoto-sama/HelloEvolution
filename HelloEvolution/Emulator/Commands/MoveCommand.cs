using System.Drawing;
using Emulator.Interfaces;

namespace Emulator.Commands
{
	public class MoveCommand: Command
	{
		public override bool IsFinal { get; } = true;

		private readonly Directions direction;

		public MoveCommand(int directionIndex) => direction = (Directions) directionIndex;

		public override void Execute(Bot bot, WorldMap map)
		{
			var movementDirectionOffset = ComputeDirectionOffset(bot.Direction, direction);
			var botPosition = bot.Position;
			botPosition.Offset(movementDirectionOffset);
			if (!map.InBounds(botPosition))
				return;
			var objAtNewPosition = map[botPosition.X, botPosition.Y].Type;
			switch (objAtNewPosition)
			{
				case WorldObjectTypes.Poison:
					bot.IsDead = true;
					break;
				case WorldObjectTypes.Food:
					bot.IncreaseHealthByFood();
					MoveBot(bot, map, botPosition);
					break;
				case WorldObjectTypes.Empty:
					MoveBot(bot, map, botPosition);
					break;
			}
			bot.MoveCommandPointer((int) objAtNewPosition);
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