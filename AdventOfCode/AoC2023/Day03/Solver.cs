using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day03;

internal class Solver(IInputDataConverter<char[][]> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<char[][], int, int>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The sum of all engine part numbers is: {SolutionValueA}";
	protected override string SolutionTextB => $"The sum of all gear ratios is: {SolutionValueB}";

	private static readonly HashSet<char> digits = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];
	private readonly Dictionary<(int, int), IList<int>> gears = [];

	protected override void SolveImplemented()
	{
		gears.Clear();
		for (int y = 0; y < inputData.Length; y++)
		{
			for (int x = 0; x < inputData[y].Length; x++)
			{
				if (digits.Contains(inputData[y][x]))
				{
					int xEnd = x + 1;
					while (xEnd < inputData[y].Length && digits.Contains(inputData[y][xEnd]))
						xEnd++;
					int partNumber = int.Parse(inputData[y][x..xEnd]);
					if (HasAdjacentSymbol(y, x, xEnd, partNumber))
						SolutionValueA += partNumber;
					x = xEnd;
				}
			}
		}
		foreach (var key in gears.Keys)
		{
			if (gears[key].Count == 2)
				SolutionValueB += gears[key][0] * gears[key][1];
		}
	}

	private bool HasAdjacentSymbol(int y, int xStart, int xEnd, int partNumber)
	{
		for (int yOff = y - 1; yOff <= y + 1; yOff++)
		{
			for (int xOff = xStart - 1; xOff <= xEnd; xOff++)
			{
				if (xOff < 0 || yOff < 0 || xOff >= inputData[0].Length || yOff >= inputData.Length)
					continue;
				if (inputData[yOff][xOff] != '.' && !digits.Contains(inputData[yOff][xOff]))
				{
					if (inputData[yOff][xOff] == '*')
					{
						if (gears.ContainsKey((xOff, yOff)))
							gears[(xOff, yOff)].Add(partNumber);
						else
						{
							gears[(xOff, yOff)] = [partNumber];
						}
					}
					return true;
				}
			}
		}
		return false;
	}
}