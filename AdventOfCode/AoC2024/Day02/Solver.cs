using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day02;

internal class Solver(IInputDataConverter<IEnumerable<int[]>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<int[]>, int?, int?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"{SolutionValueA} of the provided reports are safe.";
	protected override string SolutionTextB => $"{SolutionValueB} of the provided reports are safe when using the dampener.";

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		foreach(int[] report in inputData)
		{
			if(SequenceIsSafe(report))
				SolutionValueA++;
			for (int i = 0; i < report.Length; i++)
			{
				int[] dampenedSequence;
				if (i == 0)
					dampenedSequence = report[1..];
				else if (i == report.Length - 1)
					dampenedSequence = report[..^1];
				else
					dampenedSequence = [.. report[..i], .. report[(i + 1)..]];
				if (SequenceIsSafe(dampenedSequence))
				{
					SolutionValueB++;
					break;
				}
			}
		}
	}

	private static bool SequenceIsSafe(int[] sequence)
	{
		bool descending = sequence[0] > sequence[1];
		for (int i = 1; i < sequence.Length; i++)
		{
			int diff = (sequence[i] - sequence[i-1]) * (descending ? -1 : 1);
			if (diff is > 3 or < 1)
			{
				return false;
			}
		}
		return true;
	}
}