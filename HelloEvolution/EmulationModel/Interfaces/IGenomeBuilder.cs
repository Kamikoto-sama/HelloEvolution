using System.Collections.Generic;
using EmulationModel.Commands;

namespace EmulationModel.Interfaces
{
	public interface IGenomeBuilder
	{
		IReadOnlyList<Command> Build();
		IReadOnlyList<Command> MutateGenome(IReadOnlyList<Command> genome);
	}
}