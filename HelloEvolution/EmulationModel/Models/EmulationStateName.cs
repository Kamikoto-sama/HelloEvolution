namespace EmulationModel.Models
{
	public enum EmulationStateName
	{
		NotInitialized,
		Initialized,
		InAction,
		PendedToPause,
		Paused,
		Finished,
		PendedToRestart
	}
}