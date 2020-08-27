using System.Collections.Generic;
using System.Linq;
using EmulationModel.Interfaces;

namespace EmulationModel
{
	public class GenerationBuilder: IGenerationBuilder
	{
		private readonly EmulationConfig config;
		private readonly IGenomeBuilder genomeBuilder;
		
		public GenerationBuilder(EmulationConfig config, IGenomeBuilder genomeBuilder)
		{
			this.config = config;
			this.genomeBuilder = genomeBuilder;
		}
		
		public ICollection<Bot> CreateInitial()
		{
			return Enumerable
				.Range(0, config.GenerationSize)
				.Select(_ => new Bot(genomeBuilder.Build(), config))
				.ToArray();
		}

		public ICollection<Bot> Rebuild(IEnumerable<Bot> survivedBots)
		{
			var mutationsCount = 0;
			return survivedBots
				.Take(config.ParentsCount)
				.SelectMany(parent => Enumerable
					.Range(0, config.GenerationSize / config.ParentsCount)
					.Select(_ =>
					{
						var genome = parent.Genome;
						var generationNumber = parent.GenerationNumber + 1;
						if (mutationsCount++ < config.MutatedBotsCount)
						{
							genome = genomeBuilder.MutateGenome(genome);
							generationNumber = 1;
						}
						return new Bot(genome, config, generationNumber);
					}))
				.ToArray();
		}
	}
}