using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day09;

public class Solver : AbstractSolver<IEnumerable<long>, long?, long?>
{
	protected override string SolutionTextA => $"The first number to not be valid is {SolutionValueA}.";

	protected override string SolutionTextB =>
		$"The sum of the lowest and highest number in the contiguous range that sums up to {SolutionValueA} is {SolutionValueB}";

	public int PreambleLength { get; set; }

	public Solver(IInputDataConverter<IEnumerable<long>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		Queue<long> previousNumbers = new();
		foreach (long preamble in inputData.Take(PreambleLength))
			previousNumbers.Enqueue(preamble);
		foreach (long x in inputData.Skip(PreambleLength))
			if (IsValid(x, previousNumbers.ToList()))
			{
				previousNumbers.Dequeue();
				previousNumbers.Enqueue(x);
			}
			else
			{
				SolutionValueA = x;
				break;
			}
		if (SolutionValueA is null)
			return;

		List<long> input = inputData.ToList();
		for (int i = 0; i < inputData.Count() - 1; i++)
		{
			int j = i + 2;
			long sum = input[i] + input[i + 1];
			long min = Math.Min(input[i], input[i + 1]);
			long max = Math.Max(input[i], input[i + 1]);
			while (sum < SolutionValueA && j < input.Count)
			{
				if (input[j] < min)
					min = input[j];
				if (input[j] > max)
					max = input[j];
				sum += input[j];
				j++;
			}
			if (sum == SolutionValueA)
			{
				SolutionValueB = min + max;
				break;
			}
		}
	}

	private static bool IsValid(long sum, List<long> previousNumbers)
	{
		for (int i = 0; i < previousNumbers.Count - 1; i++)
			for (int j = i + 1; j < previousNumbers.Count; j++)
				if (previousNumbers[i] + previousNumbers[j] == sum)
					return true;
		return false;
	}
}