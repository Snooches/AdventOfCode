using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day16;

public class Solver : AbstractSolver<(Dictionary<string, IEnumerable<int>>, IList<int>, IEnumerable<IList<int>>), int, long?>
{
	protected override string SolutionTextA => $"The ticket scanning error rate is {SolutionValueA}.";
	protected override string SolutionTextB => $"The departure values multiplied together result in {SolutionValueB}.";

	public Solver(IInputDataConverter<(Dictionary<string, IEnumerable<int>>, IList<int>, IEnumerable<IList<int>>)> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	private readonly List<IList<int>> validTickets = new();

	private readonly Dictionary<string, List<int>> keyIndices = new();

	private Dictionary<string, IEnumerable<int>> rules = new();
	private IList<int> myTicket = new List<int>();
	private IEnumerable<IList<int>> ticketList = new List<IList<int>>();

	protected override void SolveImplemented()
	{
		(rules, myTicket, ticketList) = inputData;

		List<int> validNumbers = new();
		foreach (IEnumerable<int> range in rules.Values)
			validNumbers.AddRange(range);
		validNumbers = validNumbers.Distinct().ToList();

		SolutionValueA = 0;
		foreach (IEnumerable<int> ticket in ticketList)
		{
			bool valid = true;
			foreach (int value in ticket)
			{
				if (!validNumbers.Contains(value))
				{
					SolutionValueA += value;
					valid = false;
				}
			}
			if (valid)
				validTickets.Add(ticket.ToList());
		}

		foreach (string key in rules.Keys)
			keyIndices[key] = Enumerable.Range(0, rules.Count).ToList();
		if (ReduceIndices())
		{
			SolutionValueB = 1;
			foreach (string key in rules.Keys)
				if (key.StartsWith("departure"))
					SolutionValueB *= myTicket[keyIndices[key].First()];
		}
	}

	private bool ReduceIndices()
	{
		bool progress = false;
		foreach (List<int> ticket in validTickets)
		{
			for (int i = 0; i < ticket.Count; i++)
			{
				foreach (string key in rules.Keys)
				{
					if (rules[key].Contains(ticket[i]))
						continue;
					if (keyIndices[key].Remove(i))
						progress = true;
					if (keyIndices[key].Count == 1)
					{
						foreach (string key2 in keyIndices.Keys)
						{
							if (key2 != key)
								if (keyIndices[key2].Remove(keyIndices[key].First()))
									progress = true;
						}
					}
				}
			}
		}

		if (keyIndices.Values.All(list => list.Count == 1))
			return true;
		else if (!progress || keyIndices.Values.Any(List => List.Count == 0))
			return false;
		else
			return ReduceIndices();
	}
}