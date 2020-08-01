using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emulator.Interfaces;

namespace Emulator
{
	public class GenerationBuilder: IGenerationBuilder
	{
		private readonly EmulationConfig emulationConfig;
		private readonly IGenomeBuilder genomeBuilder;
		
		public GenerationBuilder(EmulationConfig emulationConfig, IGenomeBuilder genomeBuilder)
		{
			this.emulationConfig = emulationConfig;
			this.genomeBuilder = genomeBuilder;
		}
		
		public IEnumerable<Bot> CreateInitial()
		{
			return Enumerable
				.Range(0, emulationConfig.GenerationSize)
				.Select(_ =>
				{
					var genome = genomeBuilder.Build();
					var initialHealth = emulationConfig.BotInitialHealth;
					return new Bot(genome, initialHealth);
				});
		}

		public IEnumerable<Bot> BuildNew(IEnumerable<Bot> survivedBots)
		{
			throw new System.NotImplementedException();
		}
	}
}