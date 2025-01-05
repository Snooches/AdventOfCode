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
		SolutionValueA = inputData.Sum(LowestTokenCost);
		SolutionValueB = inputData.Select(x=>x with 
																					 {Prize = new Point<long>(X: x.Prize.X + 10000000000000, 
																																		Y: x.Prize.Y + 10000000000000)})
															.Sum(LowestTokenCost);
	}

	private static long LowestTokenCost(ClawMachine machine)
	{
		decimal b = Math.Round(
			(machine.Prize.Y - machine.Prize.X * machine.ButtonA.Y / (decimal)machine.ButtonA.X) /
			(machine.ButtonB.Y - machine.ButtonB.X * machine.ButtonA.Y / (decimal)machine.ButtonA.X));
								
		decimal a = Math.Round((machine.Prize.X - b * machine.ButtonB.X) / machine.ButtonA.X);

		decimal cost = a * 3 + b;

		if (machine.ButtonA.X * a + machine.ButtonB.X * b == machine.Prize.X &&
				machine.ButtonA.Y * a + machine.ButtonB.Y * b == machine.Prize.Y)
			return (long)cost;
		return 0;
	}
}