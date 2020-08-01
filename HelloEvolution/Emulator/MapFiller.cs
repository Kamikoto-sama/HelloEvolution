using System;
using System.Collections.Generic;
using System.Drawing;
using Emulator.Interfaces;

namespace Emulator
{
	public class MapFiller: IWorldMapFiller
	{
		private readonly EmulationConfig config;

		public MapFiller(EmulationConfig config)
		{
			this.config = config;
		}
		
		public void FillItems(WorldMap map)
		{
			PlaceItem(WorldObjectTypes.Food, config.FoodCountInMap, map);
			PlaceItem(WorldObjectTypes.Poison, config.PoisonCountInMap, map);
		}

		private static void PlaceItem(WorldObjectTypes objectType, int count, WorldMap map)
		{
			var attemptsCount = 0;
			var objPlacedCount = 0;
			var random = new Random();
			while (attemptsCount++ <= map.Width * map.Height / 2 && objPlacedCount < count)
			{
				var xPos = random.Next(0, map.Width);
				var yPos = random.Next(0, map.Height);
				var objPosition = new Point(xPos, yPos);
				if (map[xPos, yPos].Type != WorldObjectTypes.Empty || !map.InBounds(objPosition))
					continue;
				var obj = new WorldObject(objPosition, objectType);
				map[xPos, yPos] = obj;
				objPlacedCount++;
				attemptsCount = 0;
			}
		}

		public void FillBots(WorldMap map, IEnumerable<Bot> bots)
		{
			throw new System.NotImplementedException();
		}
	}
}