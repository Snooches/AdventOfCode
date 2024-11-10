using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day06;

internal class Solver(IInputDataConverter<(IEnumerable<Race>, Race)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(IEnumerable<Race>, Race), long, long>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The product of all winning races is: {SolutionValueA}";
	protected override string SolutionTextB => $"Number of possibles ways to break the record in the long race: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = inputData.Item1.Select(x => CalculateMargin(x)).Aggregate(1, (long x, long y) => x * y);
		SolutionValueB = CalculateMargin(inputData.Item2);
	}

	private static long CalculateMargin(Race race)
	{
		long lowestWinningTime;
		for (lowestWinningTime = 1; lowestWinningTime <= race.Time / 2; lowestWinningTime++)
			if (lowestWinningTime * (race.Time - lowestWinningTime) > race.Distance)
				break;
		long margin = race.Time + 1 - lowestWinningTime * 2;
		return margin > 0 ? margin : 0;
	}
}