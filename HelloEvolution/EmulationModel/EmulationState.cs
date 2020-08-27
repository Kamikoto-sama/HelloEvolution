using System;

namespace EmulationModel
{
	public class EmulationState
	{
		public bool Initialized { get; private set; }
		public bool InAction { get; private set; }
		public bool Paused { get; private set; }
		public bool Pending { get; private set; }
		public EmulationStateName Name { get; private set; }

		public event Action<EmulationStateName> Changed;
		
		public void Change(EmulationStateName stateName, bool invokeEvent = true)
		{
			switch (stateName)
			{
				case EmulationStateName.NotInitialized:
					Initialized = InAction = Paused = Pending = false;
					ChangeAndInvokeEvent(stateName, invokeEvent);
					break;
				case EmulationStateName.Initialized:
					Initialized = true;
					ChangeAndInvokeEvent(stateName, invokeEvent);
					break;
				case EmulationStateName.InAction when !Pending:
					InAction = true;
					Paused = false;
					ChangeAndInvokeEvent(stateName, invokeEvent);
					break;
				case EmulationStateName.PendedToPause:
					Pending = true;
					ChangeAndInvokeEvent(stateName, invokeEvent);
					break;
				case EmulationStateName.Paused:
					InAction = false;
					Paused = true;
					Pending = false;
					ChangeAndInvokeEvent(stateName, invokeEvent);
					break;
				case EmulationStateName.Finished:
					InAction = false;
					ChangeAndInvokeEvent(stateName, invokeEvent);
					break;
				case EmulationStateName.PendedToRestart:
					Pending = true;
					ChangeAndInvokeEvent(stateName, invokeEvent);
					break;
			}
		}

		private void ChangeAndInvokeEvent(EmulationStateName stateName, bool invokeEvent)
		{
			Name = stateName;
			if (invokeEvent)
				Changed?.Invoke(stateName);
		}
	}
}