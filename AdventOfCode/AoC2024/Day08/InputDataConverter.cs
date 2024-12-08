using Utilities.Interfaces;

namespace AoC2024.Day08;

internal class InputDataConverter : IInputDataConverter<(IEnumerable<List<(int,int)>>,int,int)>
{
	public (IEnumerable<List<(int,int)>>,int,int) ConvertInputData(IFileReader fileReader)
	{
		Dictionary<char, List<(int, int)>> antennas = [];
		int y = 0;
		IEnumerable<string> lines = fileReader.ReadLines();
		foreach (string line in lines)
		{
			int x = 0;
			foreach (char space in line)
			{
				if (space != '.')
					if (antennas.TryGetValue(space, out List<(int, int)>? positions))
						positions.Add((x, y));
					else
						antennas[space] = [(x, y)];
				x++;
			}
			y++;
		}
		return (antennas.Values, lines.First().Length-1, lines.Count()-1);
	}
}