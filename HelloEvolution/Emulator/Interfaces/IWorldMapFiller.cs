using System.Collections.Generic;

namespace Emulator.Interfaces
{
	public interface IWorldMapFiller
	{
		void FillItems(WorldMap map);
		void FillBots(WorldMap map, IEnumerable<Bot> bots);
	}
}