using Utilities.Interfaces;

namespace AoC2024.Day04;

internal class InputDataConverter : IInputDataConverter<Dictionary<(int, int), char>>
{
	public Dictionary<(int, int), char> ConvertInputData(IFileReader fileReader)
	{
		Dictionary<(int, int), char> grid = [];
		int x = 0;
		int y = 0;
		foreach (string line in fileReader.ReadLines())
		{
			foreach (char c in line)
			{
				grid[(x, y)] = c;
				x++;
			}
			y++;
			x = 0;
		}
		return grid;
	}
}