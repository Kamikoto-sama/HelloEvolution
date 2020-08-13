using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel.Commands
{
	public abstract class Command
	{
		public abstract bool IsFinal { get; }
		public abstract void Execute(Bot bot, WorldMap map);

		private static readonly Point[] DirectionsOffsets =
		{
			new Point(0, -1), 
			new Point(1, -1), 
			new Point(1, 0), 
			new Point(1, 1), 
			new Point(0, 1), 
			new Point(-1, 1), 
			new Point(-1, 0), 
			new Point(-1, -1), 
		};
		
		protected static IWorldMapCell GetObjByBotDirection(Directions botDirection, 
			Directions actionDirection, Point botPosition, WorldMap map)
		{
			var botDirectionIndex = (int) botDirection;
			var actionDirectionIndex = (int) actionDirection;
			var resultDirectionIndex = (botDirectionIndex + actionDirectionIndex) % DirectionsOffsets.Length;
			var resultDirection = DirectionsOffsets[resultDirectionIndex];
			botPosition.Offset(resultDirection);
			return map.InBounds(botPosition) 
				? map[botPosition] 
				: new WorldMapCell(botPosition, WorldObjectTypes.Wall);
		}
	}
}