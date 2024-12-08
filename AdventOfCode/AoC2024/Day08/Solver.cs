using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day08;

internal class Solver(IInputDataConverter<(IEnumerable<List<(int,int)>>,int,int)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(IEnumerable<List<(int,int)>>,int,int), int?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		HashSet<(int, int)> antinodesA = [];
		HashSet<(int, int)> antinodesB = [];
		foreach(List<(int X,int Y)> antennas in inputData.Item1)
			for (int i = 0; i < antennas.Count -1; i++)
				for (int j = i+1; j < antennas.Count; j++)
				{
					int xDif = antennas[i].X - antennas[j].X;
					int yDif = antennas[i].Y - antennas[j].Y;
					(int X, int Y) antinode = (antennas[i].X + xDif, antennas[i].Y + yDif);
					if(antinode.X >= 0 && antinode.X <= inputData.Item2 && antinode.Y >= 0 && antinode.Y <= inputData.Item3)
						antinodesA.Add(antinode);
					antinode = (antennas[j].X - xDif, antennas[j].Y - yDif);
					if (antinode.X >= 0 && antinode.X <= inputData.Item2 && antinode.Y >= 0 && antinode.Y <= inputData.Item3)
						antinodesA.Add(antinode);
					int multiplier = 1;
					antinode = antennas[i];
					while (antinode.X >= 0 && antinode.X <= inputData.Item2 && antinode.Y >= 0 && antinode.Y <= inputData.Item3)
					{
						antinodesB.Add(antinode);
						antinode = (antennas[i].X + xDif * multiplier, antennas[i].Y + yDif * multiplier);
						multiplier++;
					}
					multiplier = 1;
					antinode = antennas[j];
					while (antinode.X >= 0 && antinode.X <= inputData.Item2 && antinode.Y >= 0 && antinode.Y <= inputData.Item3)
					{
						antinodesB.Add(antinode);
						antinode = (antennas[j].X - xDif * multiplier, antennas[j].Y - yDif * multiplier);
						multiplier++;
					}
				}
		SolutionValueA = antinodesA.Count;
		SolutionValueB = antinodesB.Count;
	}
}