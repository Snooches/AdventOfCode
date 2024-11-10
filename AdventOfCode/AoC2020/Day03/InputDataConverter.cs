using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day03;

public class InputDataConverter : IInputDataConverter<bool[,]>
{
	public bool[,] ConvertInputData(IFileReader fileReader)
	{
		List<string> inputLines = fileReader.ReadLines().ToList();
		if (inputLines.Count == 0)
			return new bool[,] { };
		bool[,] result = new bool[inputLines[1].Length, inputLines.Count];

		for (int y = 0; y < inputLines.Count; y++)
		{
			if (inputLines[y].Length != result.GetLength(0))
				throw new InvalidInputException($"Line {y} does not match expected row length.");
			for (int x = 0; x < inputLines[y].Length; x++)
			{
				result[x, y] = inputLines[y][x] == '#';
			}
		}
		return result;
	}
}