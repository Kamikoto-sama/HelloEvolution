using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EmulationModel.Interfaces;

namespace EmulationModel
{
	public class WorldMap
	{
		public int Width { get; }
		public int Height { get; }
		private readonly ConcurrentDictionary<Point, IWorldMapCell> objects;
		public ConcurrentDictionary<WorldObjectType, int> PlacedObjectsCounts { get; }
		public ConcurrentDictionary<Point, IWorldMapCell> EmptyCells { get; }

		public event Action<WorldMapChangedEvent> CellChanged;

		public WorldMap(int width, int height)
		{
			Width = width;
			Height = height;
			objects = new ConcurrentDictionary<Point, IWorldMapCell>();
			PlacedObjectsCounts = new ConcurrentDictionary<WorldObjectType, int>();
			EmptyCells = new ConcurrentDictionary<Point, IWorldMapCell>();
		}

		public IWorldMapCell this[Point coordinates]
		{
			get => objects[coordinates];
			set
			{
				objects.TryGetValue(coordinates, out var prevObj);
				objects[coordinates] = value;
				var eventArgs = new WorldMapChangedEvent(coordinates, prevObj);
				if (value.Type != WorldObjectType.Wall)
					SaveReplacedObject(prevObj, value);
				CellChanged?.Invoke(eventArgs);
			}
		}

		public IWorldMapCell this[int x, int y]
		{
			get => this[new Point(x, y)];
			set => this[new Point(x, y)] = value;
		}

		private void SaveReplacedObject(IWorldMapCell prevObj, IWorldMapCell newObj)
		{
			if (prevObj != null && PlacedObjectsCounts.ContainsKey(prevObj.Type))
			{
				PlacedObjectsCounts[prevObj.Type]--;
				if (prevObj.Type == WorldObjectType.Empty)
					EmptyCells.TryRemove(prevObj.Position, out _);
			}
			if (!PlacedObjectsCounts.ContainsKey(newObj.Type))
				PlacedObjectsCounts[newObj.Type] = 0;
			PlacedObjectsCounts[newObj.Type]++;
			if (newObj.Type == WorldObjectType.Empty)
				EmptyCells[newObj.Position] = newObj;
		}

		public bool InBounds(Point point) =>
			!(point.X >= Width || point.X < 0 ||
			  point.Y >= Height || point.Y < 0);

		public bool InBounds(int x, int y) => InBounds(new Point(x, y));
	}
}