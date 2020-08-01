namespace Emulator
{
	public class EmulationConfig
	{
		public int GenerationSize { get; set; } = 64;
		public int GenomeSize { get; set; } = 64;
		public int BotInitialHealth { get; set; } = 40;
		public int BotMaxHealth { get; set; } = 100;
		public string TxtMapFilePath { get; set; } = "map.txt";
		public int PoisonCountInMap { get; set; } = 50;
		public int FoodCountInMap { get; set; } = 50;
	}
}