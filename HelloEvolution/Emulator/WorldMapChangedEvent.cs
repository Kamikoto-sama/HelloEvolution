using System.Drawing;
using Emulator.Interfaces;

namespace Emulator
{
	public class WorldMapChangedEvent
	{
		public Point Coordinates { get; }
		public IWorldObject PreviousCellObject { get; }

		public WorldMapChangedEvent(Point coordinates, IWorldObject previousCellObject)
		{
			Coordinates = coordinates;
			PreviousCellObject = previousCellObject;
		}
	}
}