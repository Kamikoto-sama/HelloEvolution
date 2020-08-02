using System.Collections.Generic;

namespace Emulator
{
	public class StateMonitor
	{
		public int GenerationNumber { get; set; }
		public int GenerationIterationNumber { get; set; }
		public int BotsAliveCount { get; set; }
		public ICollection<Bot> SurvivedBots { get; set; }
	}
}