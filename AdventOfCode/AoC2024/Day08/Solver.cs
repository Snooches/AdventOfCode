using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day08;

internal class Solver(IInputDataConverter<(IEnumerable<List<Point<int>>>, int, int)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(IEnumerable<List<Point<int>>> AntennaGroups, int MaxX, int MaxY), int?, int?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"{SolutionValueA} location contain antinodes.";
	protected override string SolutionTextB => $"{SolutionValueB} locations contain antinodes when considering resonant harmonics.";

	protected override void SolveImplemented()
	{
		HashSet<Point<int>> antinodesA = [];
		HashSet<Point<int>> antinodesB = [];
		foreach (List<Point<int>> antennas in inputData.AntennaGroups)
			for (int i = 0; i < antennas.Count - 1; i++)
				for (int j = i + 1; j < antennas.Count; j++)
				{
					Vector<int> diff = antennas[i] - antennas[j];

					Point<int> antinode = antennas[i] - diff;
					if (IsInGrid(antinode))
						antinodesA.Add(antinode);

					antinode = antennas[j] + diff;
					if (IsInGrid(antinode))
						antinodesA.Add(antinode);

					int multiplier = 1;
					antinode = antennas[i];
					while (IsInGrid(antinode))
					{
						antinodesB.Add(antinode);
						antinode = antennas[i] + diff * multiplier;
						multiplier++;
					}

					multiplier = 1;
					antinode = antennas[j];
					while (IsInGrid(antinode))
					{
						antinodesB.Add(antinode);
						antinode = antennas[j] - diff * multiplier;
						multiplier++;
					}
				}
		SolutionValueA = antinodesA.Count;
		SolutionValueB = antinodesB.Count;
	}

	private bool IsInGrid(Point<int> p)
	{
		return p.X >= 0 && p.X <= inputData.MaxX && p.Y >= 0 && p.Y <= inputData.MaxY;
	}
}