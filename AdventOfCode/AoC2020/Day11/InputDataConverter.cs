using Utilities.Interfaces;

namespace AoC2020.Day11;

public class InputDataConverter : IInputDataConverter<IEnumerable<(int, int, char)>>
{
	public IEnumerable<(int, int, char)> ConvertInputData(IFileReader fileReader)
	{
		List<string> input = fileReader.ReadLines().ToList();
		for (int y = 0; y < input.Count(); y++)
			for (int x = 0; x < input[y].Length; x++)
				if (".L#".Contains(input[y][x]))
					yield return (x, y, input[y][x]);
	}
}