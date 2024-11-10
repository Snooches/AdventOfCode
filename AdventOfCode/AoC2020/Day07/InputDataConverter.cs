using System.Text.RegularExpressions;
using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day07;

public class InputDataConverter : IInputDataConverter<IEnumerable<DirectedWeightedGraphNode<string, string>>>
{
	private readonly Regex FormatVerifier = new("^.* bags contain ((\\d+.*bag(s)?(, )?)+|no other bags).$");

	public IEnumerable<DirectedWeightedGraphNode<string, string>> ConvertInputData(IFileReader fileReader)
	{
		//Create NodeList
		Dictionary<string, DirectedWeightedGraphNode<string, string>> createdNodes = new();
		foreach (string line in fileReader.ReadLines())
		{
			if (!FormatVerifier.Match(line).Success)
				continue;
			string key = line[..(line.IndexOf("bags") - 1)];
			createdNodes.Add(key, new DirectedWeightedGraphNode<string, string>(key));
		}

		//Connect Nodes
		foreach (string line in fileReader.ReadLines())
		{
			if (!FormatVerifier.Match(line).Success)
				continue;
			string key = line[..(line.IndexOf("bags") - 1)];
			string[] children = line[(line.IndexOf("contain") + 8)..].Split(',');
			DirectedWeightedGraphNode<string, string> node = createdNodes[key];
			foreach (string child in children)
			{
				var split = child.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				if (split[0] != "no")
				{
					int quantifier = Int32.Parse(split[0]);
					string childKey = String.Join(" ", split[1..^1]);
					node.AddChild(createdNodes[childKey], quantifier);
				}
			}
		}
		return createdNodes.Values;
	}
}