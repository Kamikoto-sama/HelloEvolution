namespace EmulationModel
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