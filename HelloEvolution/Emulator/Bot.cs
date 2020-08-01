using System.Collections.Generic;
using System.Drawing;
using Emulator.Interfaces;

namespace Emulator
{
	public class Bot: IWorldObject
	{
		public IReadOnlyList<Command> Genome { get; }
		public Point Position { get; set; }
		public WorldObjectTypes Type { get; } = WorldObjectTypes.Bot;
		public Directions DirectionDirection { get; set; }
		public int Health { get; set; }
		public bool IsDead => Health == 0;
		public Command CurrentCommand => Genome[currentCommandIndex];
		private int currentCommandIndex;

		public Bot(IReadOnlyList<Command> genome, EmulationConfig config)
		{
			Genome = genome;
			Health = config.BotInitialHealth;
			DirectionDirection = config.BotInitialDirectionDirection;
		}

		public void MoveCommandPointer(int offsetValue) => 
			currentCommandIndex = (currentCommandIndex + offsetValue) % Genome.Count;
	}
}