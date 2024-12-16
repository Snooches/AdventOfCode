﻿using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day15;

internal class InputDataConverter : IInputDataConverter<(Dictionary<Point,StorageContent>,IEnumerable<Vector>)>
{
	public (Dictionary<Point,StorageContent>,IEnumerable<Vector>) ConvertInputData(IFileReader fileReader)
	{
		Dictionary<Point, StorageContent> storageMap = [];
		List<Vector> movements = [];

		int y = 0;
		foreach (string line in fileReader.ReadLines())
		{
			if (line.Length < 1)
				continue;

			if (line[0] == '#')
			{
				int x = 0;
				foreach (char c in line)
				{
					StorageContent content = c switch
					{
						'#' => StorageContent.Wall,
						'O' => StorageContent.Box,
						'@' => StorageContent.Robot,
						_ => StorageContent.Empty
					};
					storageMap.Add(new Point(x,y), content);
					x++;
				}
				y++;
			}
			else
			{
				foreach (char c in line)
				{
					Vector direction = c switch
					{
						'<' => new Vector(-1, 0),
						'>' => new Vector(1, 0),
						'^' => new Vector(0, -1),
						_ => new Vector(0, 1)
					};
					movements.Add(direction);
				}
			}
		}
		return (storageMap, movements);
	}
}