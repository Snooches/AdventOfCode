using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day08;

internal class InputDataConverter : IInputDataConverter<(IEnumerable<List<Point<int>>>,int,int)>
{
	public (IEnumerable<List<Point<int>>>,int,int) ConvertInputData(IFileReader fileReader)
	{
		Dictionary<char, List<Point<int>>> antennas = [];
		int y = 0;
		IEnumerable<string> lines = fileReader.ReadLines();
		foreach (string line in lines)
		{
			int x = 0;
			foreach (char space in line)
			{
				if (space != '.')
					if (antennas.TryGetValue(space, out List<Point<int>>? positions))
						positions.Add(new(x, y));
					else
						antennas[space] = [new(x, y)];
				x++;
			}
			y++;
		}
		return (antennas.Values, lines.First().Length-1, lines.Count()-1);
	}
}