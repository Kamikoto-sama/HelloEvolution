using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using EmulationModel.Interfaces;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.Models
{
	public class WorldMap : IEnumerable<KeyValuePair<Point, IWorldMapObject>>
	{
		public int Width { get; }
		public int Height { get; }
		private readonly ConcurrentDictionary<Point, IWorldMapObject> objects;
		internal ConcurrentDictionary<WorldObjectType, int> PlacedObjectsCounts { get; }

		public event Action<WorldMapCellChangedEventArgs> CellChanged;

		public IWorldMapObject this[Point coordinates]
		{
			get => objects[coordinates];
			set
			{
				objects.TryGetValue(coordinates, out var prevObj);
				objects[coordinates] = value;
				var eventArgs = new WorldMapCellChangedEventArgs(coordinates, prevObj);
				if (value.Type != WorldObjectType.Wall)
					SaveReplacedObject(prevObj, value);
				CellChanged?.Invoke(eventArgs);
			}
		}

		public IWorldMapObject this[int x, int y]
		{
			get => this[new Point(x, y)];
			set => this[new Point(x, y)] = value;
		}

		public WorldMap(int width, int height)
		{
			Width = width;
			Height = height;
			objects = new ConcurrentDictionary<Point, IWorldMapObject>();
			PlacedObjectsCounts = new ConcurrentDictionary<WorldObjectType, int>();
		}

		private void SaveReplacedObject(IWorldMapObject prevObj, IWorldMapObject newObj)
		{
			if (prevObj != null && PlacedObjectsCounts.ContainsKey(prevObj.Type))
				PlacedObjectsCounts[prevObj.Type]--;
			if (!PlacedObjectsCounts.ContainsKey(newObj.Type))
				PlacedObjectsCounts[newObj.Type] = 0;
			PlacedObjectsCounts[newObj.Type]++;
		}

		public bool InBounds(Point point) =>
			!(point.X >= Width || point.X < 0 ||
			  point.Y >= Height || point.Y < 0);

		public bool InBounds(int x, int y) => InBounds(new Point(x, y));

		public IEnumerator<KeyValuePair<Point, IWorldMapObject>> GetEnumerator() => objects.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}