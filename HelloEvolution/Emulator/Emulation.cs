using System.Collections.Generic;
using Emulator.Interfaces;

namespace Emulator
{
	public class Emulation
	{
		private readonly IWorldMapProvider mapProvider;
		private readonly IWorldMapFiller mapFiller;
		private readonly IGenerationBuilder generationBuilder;
		private WorldMap map;
		private IEnumerable<Bot> bots;

		public Emulation(IWorldMapProvider mapProvider, 
			IWorldMapFiller mapFiller,
			IGenerationBuilder generationBuilder)
		{
			this.mapProvider = mapProvider;
			this.mapFiller = mapFiller;
			this.generationBuilder = generationBuilder;
		}

		public void Prepare()
		{
			if (map != null && bots != null)
				return;
			bots = generationBuilder.CreateInitial();
			map = mapProvider.GetMap();
			mapFiller.FillItems(map);
			mapFiller.FillBots(map, bots);
		}

		public void Start()
		{
			if (map == null || bots == null)
				Prepare();
			
		}
	}
}