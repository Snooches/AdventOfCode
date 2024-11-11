using Utilities;
using Utilities.Interfaces;

namespace AoC2018.Day01;

internal class Solver(IInputDataConverter<IEnumerable<int>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<int>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The frequency after all shift is: {SolutionValueA}";
	protected override string SolutionTextB => $"The first repeated frequency is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		HashSet<long> visitedFrequencies = [0];
		long currentFrequency = 0;
		while (SolutionValueB is null)
		{
			foreach (int shift in inputData)
			{
				currentFrequency += shift;
				if (!visitedFrequencies.Contains(currentFrequency))
				{
					visitedFrequencies.Add(currentFrequency);
				}
				else
				{
					SolutionValueB ??= currentFrequency;
				}
			}
			SolutionValueA ??= currentFrequency;
		}
	}
}