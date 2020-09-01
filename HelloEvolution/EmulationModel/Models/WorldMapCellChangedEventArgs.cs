using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel.Models
{
	public class WorldMapCellChangedEventArgs
	{
		public Point Coordinates { get; }
		public IWorldMapObject PreviousObj { get; }

		public WorldMapCellChangedEventArgs(Point coordinates, IWorldMapObject previousObj)
		{
			Coordinates = coordinates;
			PreviousObj = previousObj;
		}
	}
}