using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day13;

internal class Solver(IInputDataConverter<IEnumerable<ClawMachine>> inputDataConverter, IFileReader fileReader)
	: AbstractSolver<IEnumerable<ClawMachine>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = inputData.Sum(x=>LowestTokenCost(x));
		SolutionValueB = inputData.Sum(x=>LowestTokenCost(x,10000000000000));
	}

	private static long LowestTokenCost(ClawMachine machine, long prizeOffset = 0)
	{
		decimal b = Math.Round(
			((machine.Prize.Y + prizeOffset) - (machine.Prize.X + prizeOffset) * machine.ButtonA.Y / (decimal)machine.ButtonA.X) /
			(machine.ButtonB.Y - machine.ButtonB.X * machine.ButtonA.Y / (decimal)machine.ButtonA.X));
								
		decimal a = Math.Round(((machine.Prize.X + prizeOffset) - b * machine.ButtonB.X) / machine.ButtonA.X);

		decimal cost = a * 3 + b;

		if (machine.ButtonA.X * a + machine.ButtonB.X * b == machine.Prize.X + prizeOffset &&
				machine.ButtonA.Y * a + machine.ButtonB.Y * b == machine.Prize.Y + prizeOffset)
			return (long)cost;
		return 0;
	}
}