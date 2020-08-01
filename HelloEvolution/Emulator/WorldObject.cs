using System.Drawing;
using Emulator.Interfaces;

namespace Emulator
{
	public class WorldObject: IWorldObject
	{
		public Point Position { get; set; }
		public WorldObjectTypes Type { get; }

		public WorldObject(Point position, WorldObjectTypes type)
		{
			Position = position;
			Type = type;
		}
	}
}