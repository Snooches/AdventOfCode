using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day16;

internal class InputDataConverter : IInputDataConverter<Dictionary<Point<int>,GridSpace>>
{
	public Dictionary<Point<int>,GridSpace> ConvertInputData(IFileReader fileReader)
	{
		Dictionary<Point<int>, GridSpace> maze = [];
		int y = 0;
		foreach (string line in fileReader.ReadLines())
		{
			int x = 0;
			foreach (char c in line)
			{
				maze[new(x, y)] = c switch
				{
					'.' => GridSpace.Empty,
					'S' => GridSpace.Start,
					'E' => GridSpace.Finish,
					_ => GridSpace.Wall
				};
				x++;
			}
			y++;
		}
		return maze;
	}
}