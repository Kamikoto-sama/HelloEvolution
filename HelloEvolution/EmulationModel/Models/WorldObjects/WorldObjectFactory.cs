using System.Drawing;
using EmulationModel.Interfaces;

namespace EmulationModel.Models.WorldObjects
{
    public class WorldObjFactory
    {
        public static IWorldMapObject GetWorldObj(WorldObjectType objectType, Point position)
        {
            switch (objectType)
            {
                case WorldObjectType.Poison:
                    return new Poison(position);
                case WorldObjectType.Wall:
                    return new Wall(position);
                case WorldObjectType.Food:
                    return new Food(position);
                case WorldObjectType.Empty:
                    return new Empty(position);
            }

            return null;
        }
    }
}