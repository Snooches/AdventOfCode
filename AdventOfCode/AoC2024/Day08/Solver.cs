using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day08;

internal class Solver(IInputDataConverter<(IEnumerable<List<Point>>, int, int)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(IEnumerable<List<Point>> AntennaGroups, int MaxX, int MaxY), int?, int?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"{SolutionValueA} location contain antinodes.";
	protected override string SolutionTextB => $"{SolutionValueB} locations contain antinodes when considering resonant harmonics.";

	protected override void SolveImplemented()
	{
		HashSet<Point> antinodesA = [];
		HashSet<Point> antinodesB = [];
		foreach (List<Point> antennas in inputData.AntennaGroups)
			for (int i = 0; i < antennas.Count - 1; i++)
				for (int j = i + 1; j < antennas.Count; j++)
				{
					Vector diff = antennas[i] - antennas[j];

					Point antinode = antennas[i] - diff;
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

	private bool IsInGrid(Point p)
	{
		return p.X >= 0 && p.X <= inputData.MaxX && p.Y >= 0 && p.Y <= inputData.MaxY;
	}
}