using System.Collections.Generic;
using Emulator.Commands;

namespace Emulator.Interfaces
{
	public interface IGenomeBuilder
	{
		IReadOnlyList<Command> Build();
	}
}