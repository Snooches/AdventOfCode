using Utilities.Interfaces;

namespace AoC2020.Day17;

public class InputDataConverter : IInputDataConverter<bool[,]>
{
	public bool[,] ConvertInputData(IFileReader fileReader)
	{
		string[] rows = fileReader.ReadLines().ToArray();
		if (rows.Length == 0)
			return new bool[0, 0];
		int cols = rows[0].Length;
		bool[,] result = new bool[cols, rows.Length];

		for (int y = 0; y < rows.Length; y++)
		{
			if (rows[y].Length != cols)
				throw new Exception("Input is not rectangular");
			for (int x = 0; x < cols; x++)
				if (rows[y][x] == '#')
					result[x, y] = true;
				else if (rows[y][x] == '.')
					result[x, y] = false;
				else
					throw new Exception("Invalid character");
		}
		return result;
	}
}