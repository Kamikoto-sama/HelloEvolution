using System.Collections.Generic;

namespace EmulationModel
{
	public class StatusMonitor
	{
		public int GenerationNumber { get; set; }
		public int GenerationIterationNumber { get; set; }
		public int BotsAliveCount { get; set; }
		public ICollection<Bot> SurvivedBots { get; set; }
		public List<int> GenIterationsStatistics { get; } = new List<int>();
	}
}