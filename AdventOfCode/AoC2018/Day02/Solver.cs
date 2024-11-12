using System.Text;
using Utilities;
using Utilities.Interfaces;

namespace AoC2018.Day02;

internal class Solver(IInputDataConverter<IEnumerable<string>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<string>, long?, string?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The checksum is: {SolutionValueA}";
	protected override string SolutionTextB => $"The common letters are: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		int twoLetterCount = 0;
		int threeLetterCount = 0;

		HashSet<string> previousBoxIds = [];

		foreach (string boxId in inputData)
		{
			Dictionary<char, byte> histogram = CalculateHistogram(boxId);
			if (histogram.ContainsValue(2))
				twoLetterCount++;
			if (histogram.ContainsValue(3))
				threeLetterCount++;

			if (SolutionValueB is null)
			{
				foreach (string previousBoxId in previousBoxIds)
				{
					string commonCharacters = RemoveDifferences(boxId, previousBoxId);
					if (commonCharacters.Length >= boxId.Length - 1)
						SolutionValueB = commonCharacters;
				}
				previousBoxIds.Add(boxId);
			}
		}
		SolutionValueA = twoLetterCount * threeLetterCount;
	}

	private static Dictionary<char, byte> CalculateHistogram(string value)
	{
		Dictionary<char, byte> result = [];

		foreach (char c in value)
		{
			if (result.ContainsKey(c))
				result[c]++;
			else
				result[c] = 1;
		}

		return result;
	}

	private static string RemoveDifferences(string leftSide, string rightSide)
	{
		StringBuilder result = new(leftSide.Length);
		int i = 0;
		while (i < leftSide.Length && i < rightSide.Length)
		{
			if (leftSide[i] == rightSide[i])
				result.Append(leftSide[i]);
			i++;
		}
		return result.ToString();
	}
}