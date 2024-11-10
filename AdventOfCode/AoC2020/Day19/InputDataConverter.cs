using Utilities.Interfaces;

namespace AoC2020.Day19;

public class InputDataConverter : IInputDataConverter<(IEnumerable<string>, IEnumerable<string>)>
{
	public (IEnumerable<string>, IEnumerable<string>) ConvertInputData(IFileReader fileReader)
	{
		bool ruleMode = true;
		List<string> rules = new();
		List<string> inputs = new();
		foreach (string line in fileReader.ReadLines())
		{
			if (String.IsNullOrWhiteSpace(line))
			{
				ruleMode = false;
				continue;
			}
			if (ruleMode)
				rules.Add(line);
			else
				inputs.Add(line);
		}
		return (rules, inputs);
	}
}