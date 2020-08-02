using System.Collections.Generic;
using System.Drawing;
using Emulator.Commands;
using Emulator.Interfaces;

namespace Emulator
{
	public class Bot: IWorldObject
	{
		private readonly EmulationConfig config;
		public IReadOnlyList<Command> Genome { get; }
		public Point Position { get; set; }
		public WorldObjectTypes Type { get; } = WorldObjectTypes.Bot;
		public Directions Direction { get; set; }
		public int Health { get; private set; }
		public bool IsDead { get; set; }
		public Command CurrentCommand => Genome[currentCommandIndex];
		private int currentCommandIndex;

		public Bot(IReadOnlyList<Command> genome, EmulationConfig config)
		{
			this.config = config;
			Genome = genome;
			Health = config.BotInitialHealth;
			Direction = config.BotInitialDirection;
		}

		public void MoveCommandPointer(int offsetValue) => 
			currentCommandIndex = (currentCommandIndex + offsetValue) % Genome.Count;

		public void IncreaseHealthByFood()
		{
			Health += config.FoodHealthIncrease + 1;
			if (Health > config.BotMaxHealth)
				Health -= Health - config.BotMaxHealth;
		}

		public void DecreaseHealth(int value)
		{
			Health -= value;
			IsDead = Health <= 0;
		}
	}
}