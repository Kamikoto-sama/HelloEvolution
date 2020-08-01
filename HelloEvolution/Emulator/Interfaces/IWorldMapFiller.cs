using System;
using System.Collections.Generic;
using System.Drawing;

namespace Emulator.Interfaces
{
	public interface IWorldMapFiller
	{
		void FillItems(WorldMap map);
		void FillBots(WorldMap map, IEnumerable<Bot> bots);
		void RemoveObjectsFromMap(IEnumerable<IWorldObject> objects, WorldMap map);
		void PlaceObject(Func<Point, IWorldObject> objFactory, int count, WorldMap map);
	}
}