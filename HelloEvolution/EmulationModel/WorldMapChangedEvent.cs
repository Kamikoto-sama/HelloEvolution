using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel
{
	public class WorldMapChangedEvent
	{
		public Point Coordinates { get; }
		public IWorldMapCell PreviousCellMapCell { get; }

		public WorldMapChangedEvent(Point coordinates, IWorldMapCell previousCellMapCell)
		{
			Coordinates = coordinates;
			PreviousCellMapCell = previousCellMapCell;
		}
	}
}