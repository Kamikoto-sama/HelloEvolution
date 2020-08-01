using System.Drawing;

namespace Emulator.Interfaces
{
	public interface IWorldObject
	{
		Point Position { get; set; }
		WorldObjectTypes Type { get; }
	}
}