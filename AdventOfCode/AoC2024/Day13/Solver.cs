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
	}

	private static int LowestTokenCost(ClawMachine machine)
	{
		float directionA = machine.ButtonA.X / (float)machine.ButtonA.Y;
		float directionB = machine.ButtonB.X / (float)machine.ButtonB.Y;
		float targetDirection = machine.Prize.X / (float)machine.Prize.Y;
		int cost = 0;
		Point currentTarget = new(0, 0);
		float currentDirection = (directionA + directionB)/2;
		while (currentTarget.X < machine.Prize.X && currentTarget.Y < machine.Prize.Y)
		{
			if ((directionA > targetDirection && currentDirection < targetDirection)
					||(directionA < targetDirection && currentDirection > targetDirection))
			{
				currentTarget += machine.ButtonA;
				cost += 3;
			}
			else
			{
				currentTarget += machine.ButtonB;
				cost += 1;
			}
			currentDirection = currentTarget.X / (float)currentTarget.Y;
		}
		return currentTarget == machine.Prize ? cost : 0;
	}
}