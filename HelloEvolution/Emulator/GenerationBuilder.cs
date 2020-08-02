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
		
		public ICollection<Bot> CreateInitial()
		{
			return Enumerable
				.Range(0, emulationConfig.GenerationSize)
				.Select(_ => new Bot(genomeBuilder.Build(), emulationConfig))
				.ToArray();
		}

		public ICollection<Bot> Rebuild(IEnumerable<Bot> survivedBots)
		{
			return survivedBots
				.SelectMany(parent => Enumerable
					.Range(0, emulationConfig.EachParentCopiesCount)
					.Select(_ => new Bot(parent.Genome, emulationConfig, parent.GenerationNumber + 1)))
				.Concat(Enumerable
					.Range(0, emulationConfig.MutationsCount)
					.Select(_ => new Bot(genomeBuilder.Build(), emulationConfig)))
				.ToArray();
		}
	}
}