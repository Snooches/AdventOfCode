using System.Text.RegularExpressions;

namespace AoC2020.Day19;

internal class RegexBuilder
{
	private readonly Dictionary<int, string> rules = new();
	private readonly bool modified;
	private readonly int maxInputLength;
	public IList<Regex> RegexList { get; } = new List<Regex>();

	public RegexBuilder(IEnumerable<string> rules, bool modified = false, int maxInputLength = Int32.MaxValue)
	{
		this.modified = modified;
		this.maxInputLength = maxInputLength;
		ConstructRegExList(rules);
	}

	private void ConstructRegExList(IEnumerable<string> ruleStrings)
	{
		foreach (string rule in ruleStrings)
		{
			var split = rule.Split(':', 2);
			if (Int32.TryParse(split[0], out int id))
				rules[id] = split[1].Trim();
		}
		foreach (string regExString in RuleToRegExString(0))
		{
			RegexList.Add(new Regex("^" + regExString + "$"));
		}
	}

	private IEnumerable<string> RuleToRegExString(int ruleId)
	{
		if (this.modified)
		{
			if (ruleId == 8)
				return Rule8ToRegExString();
			if (ruleId == 11)
				return Rule11ToRegExString();
		}
		List<string> resultStrings = new() { "" };
		string rule = rules[ruleId];
		string[] split = rule.Split();
		foreach (string part in split)
		{
			if (Int32.TryParse(part, out int subRule))
			{
				List<string> newResults = new();
				foreach (string regExString in RuleToRegExString(subRule))
				{
					foreach (string resultOld in resultStrings)
						newResults.Add(resultOld + regExString);
				}
				resultStrings = newResults;
			}
			else
			{
				List<string> newResults = new();
				foreach (string resultOld in resultStrings)
					newResults.Add(resultOld + part.Trim('"'));
				resultStrings = newResults;
			}
		}
		if (rule.Contains('|'))
		{
			List<string> newResults = new();
			foreach (string resultOld in resultStrings)
				newResults.Add('(' + resultOld + ')');
			resultStrings = newResults;
		}
		return resultStrings;
	}

	private IEnumerable<string> Rule8ToRegExString()
	{
		foreach (string baseString in RuleToRegExString(42))
			yield return "(" + baseString + ")+";
	}

	private IEnumerable<string> Rule11ToRegExString()
	{
		//42 31 | 42 11 31
		List<string> resultStrings = new();
		IEnumerable<string> Rule42 = RuleToRegExString(42);
		IEnumerable<string> Rule31 = RuleToRegExString(31);
		foreach (string r42 in Rule42)
			foreach (string r31 in Rule31)
				resultStrings.Add(r42 + r31);
		int reps = this.maxInputLength / (GetMinMatchLength(Rule42) + GetMinMatchLength(Rule31)) + 1;
		foreach (int i in Enumerable.Range(0, reps))
		{
			IEnumerable<string> oldResults = resultStrings.ToList();
			foreach (string r42 in Rule42)
				foreach (string r31 in Rule31)
					foreach (string old in oldResults)
						resultStrings.Add(r42 + old + r31);
			reps++;
		}
		return resultStrings;
	}

	private int GetMinMatchLength(IEnumerable<string> regExStrings)
	{
		return regExStrings.Select((str) => GetMinMatchLength(str)).Min();
	}

	private int GetMinMatchLength(string regExString)
	{
		int min = Int32.MaxValue;
		int current = 0;
		int index = 0;
		while (index < regExString.Length)
		{
			switch (regExString[index])
			{
				case '(':
					int openParantheses = 1;
					int offset = 1;
					while (index + offset < regExString.Length)
					{
						if (regExString[index + offset] == '(')
							openParantheses++;
						if (regExString[index + offset] == ')')
						{
							openParantheses--;
							if (openParantheses == 0)
								break;
						}
						offset++;
					}
					current += GetMinMatchLength(regExString[(index + 1)..(index + offset)]);
					index += offset + 1;
					break;

				case '|':
					min = Math.Min(min, current);
					current = 0;
					index++;
					break;

				case '+':
					index++;
					break;

				default:
					current++;
					index++;
					break;
			}
		}
		return Math.Min(min, current);
	}
}