using System.Collections.Generic;
using System.Drawing;
using Emulator.Interfaces;

namespace Emulator
{
	public class Bot: IWorldObject
	{
		public IReadOnlyList<Command> Genome { get; }
		public int CurrentCommandIndex { get; set; } = 0;
		public Point Position { get; set; }
		public WorldObjectTypes Type { get; } = WorldObjectTypes.Bot;
		public Point Direction { get; set; } = Point.Empty;
		public int Health { get; set; }
		public bool IsDead => Health == 0;

		public Bot(IReadOnlyList<Command> genome, int initialHealth)
		{
			Genome = genome;
			Health = initialHealth;
		}
	}
}