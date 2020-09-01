using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel.Models.WorldObjects
{
    public class Poison : IWorldMapObject
    {
        public Point Position { get; }
        public WorldObjectType Type { get; } = WorldObjectType.Poison;

        public Poison(Point position) => Position = position;
    }
}