using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day01;

public class Solver : AbstractSolver<IEnumerable<int>, int?, int?>
{
	public int Target { get; set; } = 0;

	public Solver(IInputDataConverter<IEnumerable<int>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override string SolutionTextA => $"{summandA1} and {summandA2} add up to {Target}. Their product is {SolutionValueA}";
	protected override string SolutionTextB => $"{summandB1}, {summandB2} and {summandB3} add up to {Target}. Their product is {SolutionValueB}";

	private int summandA1 = 0;
	private int summandA2 = 0;
	private int summandB1 = 0;
	private int summandB2 = 0;
	private int summandB3 = 0;

	protected override void SolveImplemented()
	{
		HashSet<int> inputSet = inputData.ToHashSet();
		foreach (int s1 in inputSet)
		{
			if (inputSet.Contains(Target - s1))
			{
				summandA1 = s1;
				summandA2 = Target - summandA1;
				SolutionValueA = summandA1 * summandA2;
				if (SolutionValueB is not null)
					return;
			}
			foreach (int s2 in inputSet)
			{
				if (inputSet.Contains(Target - s1 - s2))
				{
					summandB1 = s1;
					summandB2 = s2;
					summandB3 = Target - summandB1 - summandB2;
					SolutionValueB = summandB1 * summandB2 * summandB3;
					if (SolutionValueA is not null)
						return;
				}
			}
		}
	}
}