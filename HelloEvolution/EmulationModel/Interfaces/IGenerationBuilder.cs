using System.Collections.Generic;

namespace EmulationModel.Interfaces
{
	public interface IGenerationBuilder
	{
		ICollection<Bot> CreateInitial();
		ICollection<Bot> Rebuild(IEnumerable<Bot> survivedBots);
	}
}