using System.Text.RegularExpressions;
using Utilities.Interfaces;

namespace AoC2020.Day06;

public class InputDataConverter : IInputDataConverter<IEnumerable<IEnumerable<string>>>
{
	public IEnumerable<IEnumerable<string>> ConvertInputData(IFileReader fileReader)
	{
		Regex inputValidator = new("^[a-z]+$");
		List<string> group = new();
		foreach (string line in fileReader.ReadLines())
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				if (group.Count > 0)
					yield return group;
				group = new();
			}
			else
			{
				if (inputValidator.IsMatch(line))
					group.Add(line);
			}
		}
		if (group.Count > 0)
			yield return group;
	}
}