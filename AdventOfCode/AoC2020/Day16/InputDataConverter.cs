using Utilities.Interfaces;

namespace AoC2020.Day16;

public class InputDataConverter : IInputDataConverter<(Dictionary<string, IEnumerable<int>>, IList<int>, IEnumerable<IList<int>>)>
{
	private Dictionary<string, IEnumerable<int>> rules = new();
	private IList<int> myTicket = new List<int>();
	private IList<IList<int>> ticketList = new List<IList<int>>();
	private bool myTicketFlag = false;

	public (Dictionary<string, IEnumerable<int>>, IList<int>, IEnumerable<IList<int>>) ConvertInputData(IFileReader fileReader)
	{
		rules = new();
		myTicket = new List<int>();
		ticketList = new List<IList<int>>();

		myTicketFlag = false;

		foreach (string line in fileReader.ReadLines())
		{
			if (String.IsNullOrWhiteSpace(line))
				continue;
			if (line == "your ticket:")
			{
				myTicketFlag = true;
				continue;
			}
			if (line == "nearby tickets:")
			{
				myTicketFlag = false;
				continue;
			}
			if (char.IsDigit(line[0]))
				ReadTicket(line);
			else
				ReadRule(line);
		}

		return (rules, myTicket, ticketList);
	}

	private void ReadTicket(string inputLine)
	{
		List<int> ticket = new();
		foreach (string number in inputLine.Split(','))
			if (Int32.TryParse(number, out int value))
				ticket.Add(value);
			else
				return;
		if (myTicketFlag)
			myTicket = ticket;
		else
			ticketList.Add(ticket);
	}

	private void ReadRule(string inputLine)
	{
		List<int> validNumbers = new();
		string[] split = inputLine.Split(':', 2);
		foreach (string rangeString in split[1].Split(" or "))
		{
			string[] minMaxSplit = rangeString.Split('-');
			if (minMaxSplit.Length == 2 && Int32.TryParse(minMaxSplit[0], out int min) && Int32.TryParse(minMaxSplit[1], out int max) && min <= max)
				validNumbers.AddRange(Enumerable.Range(min, (max - min + 1)));
			else
				return;
		}
		if (!rules.ContainsKey(split[0]))
			rules.Add(split[0], validNumbers);
	}
}