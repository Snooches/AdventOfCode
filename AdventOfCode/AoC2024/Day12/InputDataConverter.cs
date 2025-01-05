namespace AoC2024.Day12;

using Utilities;
using Utilities.Interfaces;

internal class InputDataConverter : IInputDataConverter<Dictionary<Point<int>, char>>
{
	public Dictionary<Point<int>, char> ConvertInputData(IFileReader fileReader)
	{
		Dictionary<Point<int>, char> result = [];
		int y = 0;
		foreach (string line in fileReader.ReadLines())
		{
			int x = 0;
			foreach (char c in line)
			{
				result[new Point<int>(x, y)] = c;
				x++;
			}
			y++;
		}
		return result;
	}
}