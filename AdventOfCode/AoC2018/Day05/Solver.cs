using Utilities;
using Utilities.Interfaces;

namespace AoC2018.Day05;

internal class Solver(IInputDataConverter<string> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<string, long?, int?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = Collapse(inputData).Length;

		for (char c = 'A'; c <= 'Z'; c++)
		{
			string reducedPolymer = String.Join(String.Empty, inputData.Where(x => x != c && x != (c + 32)));
			SolutionValueB = Math.Min(SolutionValueB ?? int.MaxValue, Collapse(reducedPolymer).Length);
		}
	}

	private static string Collapse(string polymer)
	{
		Stack<char> collapsedPolymer = new();
		for (int i = 0; i < polymer.Length; i++)
		{
			if (collapsedPolymer.Count == 0
				|| (collapsedPolymer.Peek() != polymer[i] + 32
				  && collapsedPolymer.Peek() != polymer[i] - 32))
			{
				collapsedPolymer.Push(polymer[i]);
				continue;
			}
			collapsedPolymer.Pop();

		}
		return new string(collapsedPolymer.ToArray());
	}
}