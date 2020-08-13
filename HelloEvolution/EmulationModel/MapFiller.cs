using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EmulationModel.Interfaces;

namespace EmulationModel
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
			IWorldMapCell foodFactory(Point pos) => new WorldMapCell(pos, WorldObjectTypes.Food);
			PlaceObject(foodFactory, config.InitialItemCountInMap[WorldObjectTypes.Food], map);
			IWorldMapCell poisonFactory(Point pos) => new WorldMapCell(pos, WorldObjectTypes.Poison);
			PlaceObject(poisonFactory, config.InitialItemCountInMap[WorldObjectTypes.Poison], map);
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

		public void RemoveObjectsFromMap(IEnumerable<IWorldMapCell> objects, WorldMap map)
		{
			foreach (var bot in objects)
			{
				var emptyObj = new WorldMapCell(bot.Position, WorldObjectTypes.Empty);
				map[bot.Position.X, bot.Position.Y] = emptyObj;
			}
		}

		public void PlaceObject(Func<Point, IWorldMapCell> objFactory, int count, WorldMap map)
		{
			var emptyCells = map.EmptyCells;
			var emptyCellsCount = map.PlacedObjectsCounts[WorldObjectTypes.Empty];
			if (emptyCellsCount < count)
				throw new Exception($"Not enough cells on the map to place {count} objs");
			foreach (var (position, _) in emptyCells.Take(count))
				map[position] = objFactory(position);
		}
	}
}