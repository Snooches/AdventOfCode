using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day09;

internal class Solver(IInputDataConverter<IEnumerable<long[]>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<long[]>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The sum of the extrapolated values is: {SolutionValueA}";
	protected override string SolutionTextB => $"The sum of the extrapolated values including backwards extrapolation is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		foreach (long[] sequence in inputData)
		{
			SolutionValueA += Interpolate(sequence);
			SolutionValueB += Interpolate(sequence, true);
		}
	}

	private static long Interpolate(long[] sequence, bool backwards = false)
	{
		if (sequence.All(x => x == 0))
			return 0;
		long[] derivative = new long[sequence.Length - 1];
		for (int i = 0; i < sequence.Length - 1; i++)
			derivative[i] = sequence[i + 1] - sequence[i];
		return backwards ? sequence[0] - Interpolate(derivative, backwards)
						 : sequence[^1] + Interpolate(derivative, backwards);
	}
}