using System.Drawing;

namespace Emulator.Commands
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
		
		protected static Point ComputeDirectionOffset(Directions botDirection, Directions actionDirection)
		{
			var botDirectionIndex = (int) botDirection;
			var actionDirectionIndex = (int) actionDirection;
			var resultDirectionIndex = (botDirectionIndex + actionDirectionIndex) % DirectionsOffsets.Length;
			return DirectionsOffsets[resultDirectionIndex];
		}
	}
}