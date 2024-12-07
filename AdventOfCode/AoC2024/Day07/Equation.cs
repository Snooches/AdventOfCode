using Utilities;

namespace AoC2024.Day07;

public class Equation(long targetValue, List<int> operands)
{
	public long TargetValue { get; set; } = targetValue;
	public List<int> Operands { get; set; } = operands;

	public long Resolve(List<Operator> operators)
	{
		if (operators.Count != Operands.Count - 1)
			throw new InvalidOperationException("Nonsensical amount of operators");
		long result = Operands[0];
		for (int i = 1; i < Operands.Count; i++)
		{
			switch (operators[i - 1])
			{
				case Operator.Add:
					result += Operands[i];
					break;

				case Operator.Multiply:
					result *= Operands[i];
					break;

				case Operator.Concatenate:
					result = result * (long)Math.Pow(10, Operands[i].ToString().Length) + Operands[i];
					break;
			}
		}
		return result;
	}

	public bool IsFulfillableWithTwoOperators()
	{
		return Selections.GetCombinations(Operands.Count - 1, [Operator.Add, Operator.Multiply]).Any(x => Resolve(x) == TargetValue);
	}

	public bool IsFulfillableWithThreeOperators()
	{
		return Selections.GetCombinations(Operands.Count - 1, [Operator.Add, Operator.Multiply, Operator.Concatenate]).Any(x => Resolve(x) == TargetValue);
	}
}