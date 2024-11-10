using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day25;

public class Solver : AbstractSolver<IEnumerable<int>, int, int>
{
	protected override string SolutionTextA => $"{SolutionValueA}";

	//public override string SolutionB => "Solution has not been implemented yet";
	public Solver(IInputDataConverter<IEnumerable<int>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader) { }

	protected override void SolveImplemented()
	{
		int loopCount = GetLoopCount(inputData.ToArray()[0]);
		SolutionValueA = Transform(inputData.ToArray()[1], loopCount);
		Console.WriteLine($"Transform {inputData.ToArray()[1]} with loop count {loopCount} yields: {SolutionValueA}");
		loopCount = GetLoopCount(inputData.ToArray()[1]);
		SolutionValueA = Transform(inputData.ToArray()[0], loopCount);
		Console.WriteLine($"Transform {inputData.ToArray()[0]} with loop count {loopCount} yields: {SolutionValueA}");
	}

	private int GetLoopCount(int key)
	{
		int result = 1;
		int value = 7;
		while (value != key)
		{
			result++;
			value = Step(value, 7);
		}
		Console.WriteLine($"Loop count of {key} is {result}.");
		return result;
	}

	private int Step(int value, int step)
	{
		long result = value * step;
		result %= 20201227;
		return (int)result;
	}

	private int Transform(int subject, int loops)
	{
		long result = 1;
		for (int i = 0; i < loops; i++)
		{
			result *= subject;
			result %= 20201227;
		}
		return (int)result;
	}
}