using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day02;

internal class Solver(IInputDataConverter<IEnumerable<Game>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<Game>, int, long>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The sum of all games with possible reveals is: {SolutionValueA}";
	protected override string SolutionTextB => $"The sum of the minimal set powers is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = 0;
		SolutionValueB = 0;
		foreach (Game game in inputData)
		{
			if (game.Reveals.All(r => r.Red <= 12 && r.Green <= 13 && r.Blue <= 14))
				SolutionValueA += game.Id;
			SolutionValueB += game.MinimumSetPower;
		}
	}
}