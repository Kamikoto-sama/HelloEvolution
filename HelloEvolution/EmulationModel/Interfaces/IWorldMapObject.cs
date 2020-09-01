using System.Drawing;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.Interfaces
{
	public interface IWorldMapObject
	{
		Point Position { get; }
		WorldObjectType Type { get; }
	}
}