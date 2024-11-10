using Utilities.Interfaces;

namespace AoC2023.Day10;

internal class InputDataConverter : IInputDataConverter<char[,]>
{
	public char[,] ConvertInputData(IFileReader fileReader)
	{
		IList<string> lines = fileReader.ReadLines().ToList();
		char[,] result = new char[lines.First().Length, lines.Count];
		for (int y = 0; y < lines.Count; y++)
			for (int x = 0; x < lines[0].Length; x++)
				result[x, y] = lines[y][x];
		return result;
	}
}