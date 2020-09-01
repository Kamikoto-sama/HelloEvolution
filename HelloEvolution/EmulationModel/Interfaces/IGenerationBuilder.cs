using System.Collections.Generic;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.Interfaces
{
	public interface IGenerationBuilder
	{
		ICollection<Bot> CreateInitial();
		ICollection<Bot> Rebuild(IEnumerable<Bot> survivedBots);
	}
}