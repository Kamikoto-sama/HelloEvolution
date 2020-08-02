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

		public event Action<WorldMapChangedEvent> CellChanged;
		public IWorldObject this[int x, int y]
		{
			get => objects[x, y];
			set
			{
				var prevValue = objects[x, y];
				objects[x, y] = value;
				var coordinates = new Point(x, y);
				var eventArgs = new WorldMapChangedEvent(coordinates, prevValue);
				CellChanged?.Invoke(eventArgs);
			}
		}

		public IWorldObject this[Point coordinates]
		{
			get => this[coordinates.X, coordinates.Y];
			set => this[coordinates.X, coordinates.Y] = value;
		}

		public bool InBounds(Point point) =>
			!(point.X >= Width || point.X < 0 ||
			  point.Y >= Height || point.Y < 0);
	}
}