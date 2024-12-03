using System.Text.RegularExpressions;
using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day03;

internal partial class Solver(IInputDataConverter<IEnumerable<string>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<string>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		Regex tokenizer = TokenizerRegex();
		Boolean multEnabled = true;
		foreach (string line in inputData)
			foreach(GroupCollection groups in tokenizer.Matches(line).Select(x=>x.Groups))
			{
				switch (groups[0].Value)
				{
					case "do()":
						multEnabled = true;
						break;
					case "don't()":
						multEnabled = false;
						break;
					default:
						int product = int.Parse(groups[1].Value) * int.Parse(groups[2].Value);
						SolutionValueA += product;
						SolutionValueB += product * (multEnabled ? 1 : 0);
						break;
				}
			}
	}

	[GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)")]
	private static partial Regex TokenizerRegex();
}