using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel.Models.WorldObjects
{
    public class Food: IWorldMapObject
    {
        public Point Position { get; }
        public WorldObjectType Type { get; } = WorldObjectType.Food;

        public Food(Point position) => Position = position;
    }
}