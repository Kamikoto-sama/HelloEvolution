using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel.Models.WorldObjects
{
    public class Empty: IWorldMapObject
    {
        public Point Position { get; }
        public WorldObjectType Type { get; } = WorldObjectType.Empty;

        public Empty(Point position) => Position = position;
    }
}