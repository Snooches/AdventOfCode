using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day04;

internal class Solver(IInputDataConverter<IEnumerable<Scratchcard>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<Scratchcard>, int, int>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The total worth of all scratchcards is: {SolutionValueA}";
	protected override string SolutionTextB => $"The total number of scratchcards gets up to: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		List<int> multiplierQueue = [];
		foreach (int matches in inputData.Select(x => x.NumberOfMatches))
		{
			int multiplier = multiplierQueue.Count > 0 ? multiplierQueue[0] : 1;
			if (multiplierQueue.Count > 0)
				multiplierQueue.RemoveAt(0);
			SolutionValueA += (int)Math.Pow(2, matches - 1);
			SolutionValueB += multiplier;
			for (int i = 0; i < matches; i++)
			{
				if (i < multiplierQueue.Count)
					multiplierQueue[i] += multiplier;
				else
					multiplierQueue.Add(multiplier + 1);
			}
		}
	}
}