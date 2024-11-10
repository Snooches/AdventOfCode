using System.Text.RegularExpressions;
using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day19;

public class Solver : AbstractSolver<(IEnumerable<string> rules, IEnumerable<string> inputs), int, int>
{
	protected override string SolutionTextA => $"{SolutionValueA} of the messages match rule 0.";
	protected override string SolutionTextB => $"{SolutionValueB} of the messages match rule 0 after updateing rules 8 and 11.";

	public Solver(IInputDataConverter<(IEnumerable<string>, IEnumerable<string>)> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		IEnumerable<Regex> r = new RegexBuilder(inputData.rules).RegexList;
		foreach (string phrase in inputData.inputs)
		{
			foreach (Regex regex in r)
				if (regex.IsMatch(phrase))
				{
					SolutionValueA++;
					break;
				}
		}

		int maxInputLength = inputData.inputs.Select((s) => s.Length).Max();
		r = new RegexBuilder(inputData.rules, true, maxInputLength).RegexList;
		foreach (string phrase in inputData.inputs)
		{
			foreach (Regex regex in r)
				if (regex.IsMatch(phrase))
				{
					SolutionValueB++;
					break;
				}
		}
	}
}