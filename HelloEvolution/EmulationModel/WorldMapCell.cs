using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel
{
	public class WorldMapCell: IWorldMapCell
	{
		public Point Position { get; set; }
		public WorldObjectTypes Type { get; }
		public bool IsBusy { get; set; }

		public WorldMapCell(Point position, WorldObjectTypes type)
		{
			Position = position;
			Type = type;
		}

		public override string ToString() => Type.ToString();
	}
}