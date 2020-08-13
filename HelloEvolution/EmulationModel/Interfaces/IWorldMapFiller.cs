using System;
using System.Collections.Generic;
using System.Drawing;

namespace EmulationModel.Interfaces
{
	public interface IWorldMapFiller
	{
		void FillItems(WorldMap map);
		void FillBots(WorldMap map, IEnumerable<Bot> bots);
		void RemoveObjectsFromMap(IEnumerable<IWorldMapCell> objects, WorldMap map);
		void PlaceObject(Func<Point, IWorldMapCell> objFactory, int count, WorldMap map);
	}
}