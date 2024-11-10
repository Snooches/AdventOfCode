using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day06;

public class Solver : AbstractSolver<IEnumerable<IEnumerable<string>>, int, int>
{
	protected override string SolutionTextA => $"The sum of group based any yes-answers is {SolutionValueA}";
	protected override string SolutionTextB => $"The sum of group based every yes-answers is {SolutionValueB}";

	public Solver(IInputDataConverter<IEnumerable<IEnumerable<string>>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		SolutionValueA = 0;
		SolutionValueB = 0;
		foreach (IEnumerable<string> grp in inputData)
		{
			SolutionValueA += string.Join(string.Empty, grp).Distinct().Count();
			if (grp.Count() > 0)
				SolutionValueB += grp.Aggregate((string1, string2) => new string(string1.Intersect(string2).ToArray())).Count();
		}
	}
}