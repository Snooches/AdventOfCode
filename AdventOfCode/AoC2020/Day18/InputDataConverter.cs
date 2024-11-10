using System.Text.RegularExpressions;
using Utilities.Interfaces;

namespace AoC2020.Day18;

public class InputDataConverter : IInputDataConverter<IEnumerable<string>>
{
	public IEnumerable<string> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			if (!line.All((c) => "0123456789+*() ".Contains(c)))
				continue;
			string result = line.Replace(" ", "");
			if (ParanthesesMismatch(result))
				continue;
			if (InvalidTermStructure(result))
				continue;
			yield return result;
		}
	}

	private static bool ParanthesesMismatch(string input)
	{
		int openParantheses = 0;
		foreach (char c in input)
		{
			if (c == '(')
				openParantheses++;
			if (c == ')')
			{
				openParantheses--;
				if (openParantheses < 0)
					return true;
			}
		}
		return openParantheses != 0;
	}

	private static bool InvalidTermStructure(string input)
	{
		string strippedInput = input.Replace("(", "").Replace(")", "");
		return !new Regex("^\\d+((\\+|\\*)\\d+)*$").IsMatch(strippedInput);
	}
}