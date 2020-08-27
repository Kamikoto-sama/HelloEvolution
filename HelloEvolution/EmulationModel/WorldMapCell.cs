using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel
{
	public class WorldMapCell: IWorldMapCell
	{
		public Point Position { get; set; }
		public WorldObjectType Type { get; }
		public bool IsBusy { get; set; }

		public WorldMapCell(Point position, WorldObjectType type)
		{
			Position = position;
			Type = type;
		}

		public override string ToString() => Type.ToString();
	}
}