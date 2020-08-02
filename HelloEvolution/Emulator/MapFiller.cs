using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
			IWorldObject foodFactory(Point pos) => new WorldObject(pos, WorldObjectTypes.Food);
			PlaceObject(foodFactory, config.InitialFoodCountInMap, map);
			IWorldObject poisonFactory(Point pos) => new WorldObject(pos, WorldObjectTypes.Poison);
			PlaceObject(poisonFactory, config.InitialPoisonCountInMap, map);
		}

		public void FillBots(WorldMap map, IEnumerable<Bot> bots)
		{
			foreach (var bot in bots)
				PlaceObject(pos =>
				{
					bot.Position = pos;
					return bot;
				}, 1, map);
		}

		public void RemoveObjectsFromMap(IEnumerable<IWorldObject> objects, WorldMap map)
		{
			foreach (var bot in objects)
			{
				var emptyObj = new WorldObject(bot.Position, WorldObjectTypes.Empty);
				map[bot.Position.X, bot.Position.Y] = emptyObj;
			}
		}

		public void PlaceObject(Func<Point, IWorldObject> objFactory, int count, WorldMap map)
		{
			var attemptsCount = 0;
			var objPlacedCount = 0;
			var random = new Random();
			while (objPlacedCount < count)
			{
				if (attemptsCount++ > map.Width * map.Height / 2)
					throw new Exception("Object placement failed");
				var xPos = random.Next(0, map.Width);
				var yPos = random.Next(0, map.Height);
				var objPosition = new Point(xPos, yPos);
				if (map[xPos, yPos].Type != WorldObjectTypes.Empty || !map.InBounds(objPosition))
					continue;
				var obj = objFactory(objPosition);
				map[xPos, yPos] = obj;
				objPlacedCount++;
				attemptsCount = 0;
			}
		}
	}
}