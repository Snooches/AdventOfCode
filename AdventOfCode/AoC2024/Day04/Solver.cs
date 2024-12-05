using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day04;

internal class Solver(IInputDataConverter<Dictionary<(int, int), char>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<Dictionary<(int, int), char>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The number of 'XMAS's is: {SolutionValueA}";
	protected override string SolutionTextB => $"The number of 'X-MAS's is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		HashSet<(int, int)> directions = [(-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1)];
		HashSet<(int, int)> diagonals = [(-1, 1),(1, 1),(1, -1),(-1, -1)];
		
		foreach ((int x, int y) in inputData.Keys)
		{
			if (inputData[(x, y)] == 'X')
				foreach ((int xDif, int yDif) in directions)
				{
					if (inputData.GetValueOrDefault((x + xDif, y + yDif)) == 'M'
						&& inputData.GetValueOrDefault((x + 2 * xDif, y + 2 * yDif)) == 'A'
						&& inputData.GetValueOrDefault((x + 3 * xDif, y + 3 * yDif)) == 'S')
						SolutionValueA++;
				}

			if (inputData[(x, y)] == 'A')
			{
				int matches = 0;
				foreach ((int xDif, int yDif) in diagonals)
					if (inputData.GetValueOrDefault((x + xDif, y + yDif)) == 'M'
						&& inputData.GetValueOrDefault((x - xDif, y - yDif)) == 'S')
						matches++;
				if (matches == 2)
					SolutionValueB++;
			}
		}
	}
}