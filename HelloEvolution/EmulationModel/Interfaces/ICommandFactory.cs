using EmulationModel.Commands;

namespace EmulationModel.Interfaces
{
	public interface ICommandFactory
	{
		int SubtypesCount { get; }
		Command Create(int subTypeIndex);
	}
}