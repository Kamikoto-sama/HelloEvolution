using System.Collections.Generic;
using System.Drawing;
using EmulationModel.Commands;
using EmulationModel.Interfaces;

namespace EmulationModel
{
	public class Bot: IWorldMapCell
	{
		private readonly EmulationConfig config;
		public IReadOnlyList<Command> Genome { get; }
		public Point Position { get; set; }
		public WorldObjectType Type { get; } = WorldObjectType.Bot;
		public bool IsBusy { get; set; }
		public Directions Direction { get; set; }
		public int Health { get; set; }
		public bool IsDead => Health <= 0;
		public Command CurrentCommand => Genome[currentCommandIndex];
		private int currentCommandIndex;
		public int GenerationNumber { get; }

		public Bot(IReadOnlyList<Command> genome, EmulationConfig config, int generationNumber=1)
		{
			this.config = config;
			Genome = genome;
			GenerationNumber = generationNumber;
			Health = config.BotInitialHealth;
		}

		public void MoveCommandPointer(int offsetValue) => 
			currentCommandIndex = (currentCommandIndex + offsetValue) % Genome.Count;

		public void IncreaseHealthByFood()
		{
			Health += config.FoodHealthIncrease + 1;
			if (Health > config.BotMaxHealth)
				Health -= Health - config.BotMaxHealth;
		}

		public override string ToString() => 
			$"{(IsDead? "Dead": Health.ToString())} " +
			$"index={currentCommandIndex} {Direction}";
	}
}