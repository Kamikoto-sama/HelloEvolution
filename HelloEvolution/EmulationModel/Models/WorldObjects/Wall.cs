using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel.Models.WorldObjects
{
    public class Wall: IWorldMapObject
    {
        public Point Position { get; }
        public WorldObjectType Type { get; } = WorldObjectType.Wall;

        public Wall(Point position) => Position = position;
    }
}