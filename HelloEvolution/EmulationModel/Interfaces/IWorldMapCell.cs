using System.Drawing;

namespace EmulationModel.Interfaces
{
	public interface IWorldMapCell
	{
		Point Position { get; set; }
		WorldObjectTypes Type { get; }
		bool IsBusy { get; set; }
	}
}