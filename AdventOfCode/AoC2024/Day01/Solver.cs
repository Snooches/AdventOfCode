using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day01;

internal class Solver(IInputDataConverter<(List<int>, List<int>)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(List<int>, List<int>), int, int>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The sum of all differences is: {SolutionValueA}";
	protected override string SolutionTextB => $"The total similarity score is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		inputData.Item1.Sort();
		inputData.Item2.Sort();
		for(int i = 0; i < inputData.Item1.Count; i++)
			SolutionValueA += Math.Abs(inputData.Item1[i] - inputData.Item2[i]);

		Dictionary<int, int> histogram = [];
		foreach(int value in inputData.Item2)
			if (histogram.TryGetValue(value, out int count))
				histogram[value] = count + 1;
			else
				histogram[value] = 1;
		foreach (int value in inputData.Item1)
			SolutionValueB += value * histogram.GetValueOrDefault(value);
	}
}