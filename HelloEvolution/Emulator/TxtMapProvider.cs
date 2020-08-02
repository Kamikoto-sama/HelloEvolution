using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Emulator.Interfaces;

namespace Emulator
{
	public class TxtMapProvider: IWorldMapProvider
	{
		private readonly EmulationConfig config;
		
		public TxtMapProvider(EmulationConfig config)
		{
			this.config = config;
		}
		
		public WorldMap GetMap()
		{
			var mapLines = File.ReadAllLines(config.TxtMapFilePath);
			if (mapLines.Length == 0)
				throw new InvalidDataException($"File {config.TxtMapFilePath} is empty");
			var mapSize = mapLines.First().Split(new[] {' ', 'x'}, StringSplitOptions.RemoveEmptyEntries);
			if (mapSize.Length != 2 || 
			    !int.TryParse(mapSize[0], out var mapWidth) ||
			    !int.TryParse(mapSize[1], out var mapHeight))
				throw new InvalidDataException($"Invalid map size format in {config.TxtMapFilePath}");
			var objects = ParseMapSchema(mapLines.Skip(1), mapWidth, mapHeight);
			return new WorldMap(objects, mapWidth, mapHeight);
		}

		private IWorldObject[,] ParseMapSchema(IEnumerable<string> mapSchema, in int mapWidth, in int mapHeight)
		{
			var objects = new IWorldObject[mapWidth, mapHeight];
			var rowIndex = 0;
			foreach (var mapRow in mapSchema)
			{
				for (var colIndex = 0; colIndex < mapWidth; colIndex++)
				{
					var objPosition = new Point(colIndex, rowIndex);
					var objType = mapRow[colIndex] == '#' ? WorldObjectTypes.Wall : WorldObjectTypes.Empty;
					objects[colIndex, rowIndex] = new WorldObject(objPosition, objType);
				}
				rowIndex++;
			}

			return objects;
		}
	}
}