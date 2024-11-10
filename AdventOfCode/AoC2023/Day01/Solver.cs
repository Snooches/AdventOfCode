using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day01;

internal class Solver(IInputDataConverter<IEnumerable<string>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<string>, int, int>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The sum of all calibration values is: {SolutionValueA}";
	protected override string SolutionTextB => $"The sum of all calibration values including spelled out digits is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		foreach (string line in inputData)
		{
			SolutionValueA += GetCalibrationValues(line);
			SolutionValueB += GetCalibrationValues(ResolveSpelledDigits(line));
		}
	}

	private static int GetCalibrationValues(string input)
	{
		int? first = null;
		int? last = null;

		foreach (char c in input)
		{
			if (int.TryParse(c.ToString(), out int digit))
			{
				first ??= digit;
				last = digit;
			}
		}
		return (first ?? 0) * 10 + last ?? 0;
	}

	private static string ResolveSpelledDigits(string input)
	{
		return input.Replace("one", "one1one")
					.Replace("two", "two2two")
					.Replace("three", "three3three")
					.Replace("four", "four4four")
					.Replace("five", "five5five")
					.Replace("six", "six6six")
					.Replace("seven", "seven7seven")
					.Replace("eight", "eight8eight")
					.Replace("nine", "nine9nine");
	}
}