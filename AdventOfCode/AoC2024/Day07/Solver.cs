using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day07;

internal class Solver(IInputDataConverter<IEnumerable<Equation>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<Equation>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The sum of all viable test values is: {SolutionValueA}";
	protected override string SolutionTextB => $"The sum of all viable test values, considering three operators, is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;

		foreach(Equation equation in inputData)
		{
			if (equation.IsFulfillableWithTwoOperators())
				SolutionValueA += equation.TargetValue;
			if (equation.IsFulfillableWithThreeOperators())
				SolutionValueB += equation.TargetValue;
		}
	}
}