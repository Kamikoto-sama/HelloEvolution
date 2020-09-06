using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EmulationModel.Interfaces;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.DefaultImplementations
{
	public class DefaultMapFiller: IWorldMapFiller
	{
		private readonly EmulationConfig config;
		private readonly Random random;

		public DefaultMapFiller(EmulationConfig config, Random random)
		{
			this.config = config;
			this.random = random;
		}

		public void FillItems(WorldMap map)
		{
			IWorldMapObject FoodFactory(Point pos) => new Food(pos);
			PlaceObject(FoodFactory, config.MaxItemsCountOnMap.Food, map);
			IWorldMapObject PoisonFactory(Point pos) => new Poison(pos);
			PlaceObject(PoisonFactory, config.MaxItemsCountOnMap.Poison, map);
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

		public void PlaceObject(Func<Point, IWorldMapObject> objFactory, int count, WorldMap map)
		{
			var emptyCellsCount = map.PlacedObjectsCounts[WorldObjectType.Empty];
			if (emptyCellsCount < count)
				throw new Exception($"Not enough cells on the map to place {count} objs");

			var placementAttemptsCount = 0;
			var objPlacedCount = 0;
			while (++placementAttemptsCount < (map.Width - 2) * (map.Height - 2) && objPlacedCount < count)
			{
				var xPos = random.Next(1, map.Width - 1);
				var yPos = random.Next(1, map.Height - 1);
				var position = new Point(xPos, yPos);
				if (map[position].Type != WorldObjectType.Empty)
					continue;
				map[position] = objFactory(position);
				objPlacedCount++;
			}
		}
	}
}