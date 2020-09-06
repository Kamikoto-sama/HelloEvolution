using System;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.Models
{
    public class ItemsSpawnSettings
    {
        public int Food { get; set; }
        public int Poison { get; set; }

        public int this[WorldObjectType itemType]
        {
            get => GetOrSetItemValue(itemType);
            set => GetOrSetItemValue(itemType, value);
        }

        private int GetOrSetItemValue(WorldObjectType itemName, int? newValue=null)
        {
            switch (itemName)
            {
                case WorldObjectType.Food:
                    Food = newValue ?? Food;
                    return Food;
                case WorldObjectType.Poison:
                    Poison = newValue ?? Poison;
                    return Poison;
            }
            throw new ArgumentException($"Unknown item name {itemName}");
        }

        public override string ToString()
        {
            var values = new[]
            {
                $"{nameof(Food)}={Food}",
                $"{nameof(Poison)}={Poison}",
            };
            return string.Join(" ", values);
        }
    }
}