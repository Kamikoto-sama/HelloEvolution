using System.Collections.Generic;

namespace Emulator.Interfaces
{
	public interface IGenomeBuilder
	{
		IReadOnlyList<Command> Build();
	}
}