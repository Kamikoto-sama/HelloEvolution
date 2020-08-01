using System.Collections.Generic;

namespace Emulator.Interfaces
{
	public interface IGenerationBuilder
	{
		IEnumerable<Bot> CreateInitial();
		IEnumerable<Bot> BuildNew(IEnumerable<Bot> survivedBots);
	}
}