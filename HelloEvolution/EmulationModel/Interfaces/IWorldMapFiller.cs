using System;
using System.Collections.Generic;
using System.Drawing;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.Interfaces
{
	public interface IWorldMapFiller
	{
		void FillItems(WorldMap map);
		void FillBots(WorldMap map, IEnumerable<Bot> bots);
		void PlaceObject(Func<Point, IWorldMapObject> objFactory, int count, WorldMap map);
	}
}