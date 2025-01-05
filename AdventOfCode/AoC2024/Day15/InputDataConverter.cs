using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day15;

internal class InputDataConverter : IInputDataConverter<(Dictionary<Point<int>,StorageContent>,IEnumerable<Vector<int>>)>
{
	public (Dictionary<Point<int>,StorageContent>,IEnumerable<Vector<int>>) ConvertInputData(IFileReader fileReader)
	{
		Dictionary<Point<int>, StorageContent> storageMap = [];
		List<Vector<int>> movements = [];

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
					storageMap.Add(new Point<int>(x,y), content);
					x++;
				}
				y++;
			}
			else
			{
				foreach (char c in line)
				{
					Vector<int> direction = c switch
					{
						'<' => new Vector<int>(-1, 0),
						'>' => new Vector<int>(1, 0),
						'^' => new Vector<int>(0, -1),
						_ => new Vector<int>(0, 1)
					};
					movements.Add(direction);
				}
			}
		}
		return (storageMap, movements);
	}
}