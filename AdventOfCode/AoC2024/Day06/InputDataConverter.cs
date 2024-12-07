using Utilities.Interfaces;

namespace AoC2024.Day06;

internal class InputDataConverter : IInputDataConverter<((int,int),Dictionary<(int,int),State>)>
{
	public ((int,int),Dictionary<(int,int),State>) ConvertInputData(IFileReader fileReader)
	{
		Dictionary<(int, int), State> grid = [];
		(int, int) startPosition = (0, 0);
		int y = 0;
		foreach (string line in fileReader.ReadLines())
		{
			int x = 0;
			foreach(char c in line)
			{
				if (c == '^')
					startPosition = (x, y);
				grid.Add((x, y), c switch
				{
					'.' => State.Empty,
					'#' => State.Blocked,
					_ => State.Empty
				});
				x++;
			}
			y++;
		}
		return (startPosition, grid);
	}
}