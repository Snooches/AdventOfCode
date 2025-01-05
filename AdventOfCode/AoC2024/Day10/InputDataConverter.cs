using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day10;

internal class InputDataConverter : IInputDataConverter<Dictionary<Point<int>,byte>>
{
	public Dictionary<Point<int>, byte> ConvertInputData(IFileReader fileReader)
	{
		Dictionary<Point<int>, byte> result = [];
		int y = 0;
		foreach (string line in fileReader.ReadLines())
		{
			int x = 0;
			foreach (char c in line)
			{
				result.Add(new(x, y), (byte)(c - 48));
				x++;
			}
			y++;
		}
		return result;
	}
}