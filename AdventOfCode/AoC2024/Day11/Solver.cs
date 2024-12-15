using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day11;

internal class Solver(IInputDataConverter<List<long>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<List<long>, int?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";


	private readonly Dictionary<long, List<long>> calculatedValues = [];
	protected override void SolveImplemented()
	{
		SolutionValueB = inputData.Sum(x => Blink(x, 25));
	}

	private long Blink(long start, int blinksLeft)
	{
		if (blinksLeft == 0)
			return start;
		if (calculatedValues.TryGetValue(start, out List<long>? nextValues) && nextValues.Count >= blinksLeft)
			return nextValues[blinksLeft - 1];
		long result = 
	}
}