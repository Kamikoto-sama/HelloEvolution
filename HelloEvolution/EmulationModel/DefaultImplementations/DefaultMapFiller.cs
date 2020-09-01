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

		public DefaultMapFiller(EmulationConfig config)
		{
			this.config = config;
		}

		public void FillItems(WorldMap map)
		{
			IWorldMapObject foodFactory(Point pos) => new Food(pos);
			PlaceObject(foodFactory, config.InitialItemCountInMap[WorldObjectType.Food], map);
			IWorldMapObject poisonFactory(Point pos) => new Poison(pos);
			PlaceObject(poisonFactory, config.InitialItemCountInMap[WorldObjectType.Poison], map);
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
			var emptyCells = map.EmptyCells;
			var emptyCellsCount = map.PlacedObjectsCounts[WorldObjectType.Empty];
			if (emptyCellsCount < count)
				throw new Exception($"Not enough cells on the map to place {count} objs");
			foreach (var (position, _) in emptyCells.Take(count))
				map[position] = objFactory(position);
		}
	}
}