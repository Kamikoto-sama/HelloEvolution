using System.Drawing;

namespace EmulationModel.Interfaces
{
	public interface IWorldMapCell
	{
		Point Position { get; set; }
		WorldObjectType Type { get; }
		bool IsBusy { get; set; }
	}
}