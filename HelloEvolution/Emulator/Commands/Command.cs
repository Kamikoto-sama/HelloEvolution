namespace Emulator
{
	public abstract class Command
	{
		public abstract bool IsFinal { get; }
		public abstract void Execute(Bot bot, WorldMap map);
	}
}