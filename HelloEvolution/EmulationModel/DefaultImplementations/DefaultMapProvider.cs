using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using EmulationModel.Interfaces;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationModel.DefaultImplementations
{
	public class DefaultMapProvider: IWorldMapProvider
	{
		private readonly EmulationConfig config;

		public DefaultMapProvider(EmulationConfig config)
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
			var map = ParseMapSchema(mapLines.Skip(1), mapWidth, mapHeight);
			return map;
		}

		private WorldMap ParseMapSchema(IEnumerable<string> mapSchema, in int mapWidth, in int mapHeight)
		{
			var map = new WorldMap(mapWidth, mapHeight);
			var rowIndex = 0;
			foreach (var mapRow in mapSchema)
			{
				for (var colIndex = 0; colIndex < mapWidth; colIndex++)
				{
					var objPosition = new Point(colIndex, rowIndex);
					IWorldMapObject worldObj;
					try
					{
						worldObj = mapRow[colIndex] == '#'
							? (IWorldMapObject) new Wall(objPosition)
							: new Empty(objPosition);
					}
					catch (IndexOutOfRangeException)
					{
						throw new InvalidDataException("Map size doesnt match map schema");
					}
					map[colIndex, rowIndex] = worldObj;
				}
				rowIndex++;
			}

			return map;
		}
	}
}