using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day07;

internal class Solver(IInputDataConverter<IEnumerable<Hand>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<Hand>, long, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The total winnings are: {SolutionValueA}";
	protected override string SolutionTextB => $"The total winnings considering the new joker rule are: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = 0;
		int index = 1;
		foreach (Hand hand in inputData.OrderBy(h => h.CalculateScore()))
		{
			SolutionValueA += hand.Bet * index;
			index++;
		}
		SolutionValueB = 0;
		index = 1;
		foreach (Hand hand in inputData.OrderBy(h => h.CalculateScore(true)))
		{
			SolutionValueB += hand.Bet * index;
			index++;
		}
	}
}