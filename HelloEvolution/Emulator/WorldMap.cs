using System;
using System.Drawing;
using Emulator.Interfaces;

namespace Emulator
{
	public class WorldMap
	{
		public int Width { get; }
		public int Height { get; }
		private readonly IWorldObject[,] objects;

		public WorldMap(IWorldObject[,] objects, int width, int height)
		{
			Width = width;
			Height = height;
			this.objects = objects;
		}

		public IWorldObject this[int x, int y]
		{
			get => objects[x, y];
			set => objects[x, y] = value;
		}

		public bool InBounds(Point point) =>
			!(point.X >= Width || point.X < 0 ||
			  point.Y >= Height || point.Y < 0);
	}
}