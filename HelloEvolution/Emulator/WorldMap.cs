using System;
using System.Collections.Generic;
using System.Drawing;
using Emulator.Interfaces;

namespace Emulator
{
	public class WorldMap
	{
		public int Width { get; }
		public int Height { get; }
		private readonly IWorldObject[,] objects;
		public Dictionary<WorldObjectTypes, int> ObjectsCounts;

		public WorldMap(IWorldObject[,] objects, int width, int height)
		{
			Width = width;
			Height = height;
			this.objects = objects;
			ObjectsCounts = new Dictionary<WorldObjectTypes, int>();
		}

		public event Action<WorldMapChangedEvent> CellChanged;
		public IWorldObject this[int x, int y]
		{
			get => objects[x, y];
			set
			{
				var prevObj = objects[x, y];
				objects[x, y] = value;
				var coordinates = new Point(x, y);
				var eventArgs = new WorldMapChangedEvent(coordinates, prevObj);
				UpdateObjectsCounts(prevObj, value);
				CellChanged?.Invoke(eventArgs);
			}
		}

		private void UpdateObjectsCounts(IWorldObject prevObj, IWorldObject newObj)
		{
			if (ObjectsCounts.ContainsKey(prevObj.Type) && ObjectsCounts[prevObj.Type] > 0)
				ObjectsCounts[prevObj.Type]--;
			if (newObj.Type == WorldObjectTypes.Wall)
				return;
			if (!ObjectsCounts.ContainsKey(newObj.Type))
				ObjectsCounts[newObj.Type] = 0;
			ObjectsCounts[newObj.Type]++;
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