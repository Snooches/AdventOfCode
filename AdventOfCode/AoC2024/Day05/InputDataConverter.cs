using Utilities.Interfaces;

namespace AoC2024.Day05;

internal class InputDataConverter : IInputDataConverter<(Dictionary<int, HashSet<int>>, HashSet<int[]>)>
{
	public (Dictionary<int, HashSet<int>>, HashSet<int[]>) ConvertInputData(IFileReader fileReader)
	{
		Dictionary<int, HashSet<int>> rules = [];
		HashSet<int[]> updates = [];
		Boolean consumingRulesInput = true;
		foreach (String line in fileReader.ReadLines())
		{
			if (line == String.Empty)
			{
				consumingRulesInput = false;
				continue;
			}
			if (consumingRulesInput)
				ConsumeRule(line, rules);
			else
				ConsumeUpdate(line, updates);
		}
		return (rules, updates);
	}

	private static void ConsumeRule(String line, Dictionary<int, HashSet<int>> rules)
	{
		Int32 predecessor = Int32.Parse(line[..2]);
		Int32 successor = Int32.Parse(line[3..]);
		if (rules.TryGetValue(predecessor, out HashSet<int>? successors))
			successors.Add(successor);
		else
			rules[predecessor] = [successor];
	}

	private static void ConsumeUpdate(String line, HashSet<int[]> updates)
	{
		updates.Add(line.Split(',').Select(x=>Int32.Parse(x)).ToArray());
	}
}