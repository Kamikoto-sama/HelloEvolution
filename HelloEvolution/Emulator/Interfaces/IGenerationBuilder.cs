using System.Collections.Generic;

namespace Emulator.Interfaces
{
	public interface IGenerationBuilder
	{
		ICollection<Bot> CreateInitial();
		ICollection<Bot> BuildNew(IEnumerable<Bot> survivedBots);
	}
}