using Utilities.Interfaces;

namespace AoC2024.Day13;

using Utilities;

internal class InputDataConverter : IInputDataConverter<IEnumerable<ClawMachine>>
{
	public IEnumerable<ClawMachine> ConvertInputData(IFileReader fileReader)
	{
		Vector<int> buttonA = new(0, 0);
		Vector<int> buttonB = new(0, 0);
		foreach (string line in fileReader.ReadLines())
			switch (line.Split(':')[0])
			{
				case "Button A":
					buttonA = ParseVector(line);
				break;
				case "Button B":
					buttonB = ParseVector(line);
				break;
				case "Prize":
					yield return new ClawMachine()
											 {
												 ButtonA = buttonA,
												 ButtonB = buttonB,
												 Prize = ParsePrize(line),
											 };
				break;
			}
	}

	private Vector<int> ParseVector(string line)
	{
		string[] parts = line.Split(':')[1].Split(',');
		return new Vector<int>(int.Parse(parts[0].Trim()[2..]), int.Parse(parts[1].Trim()[2..]));
	}

	private Point<long> ParsePrize(string line)
	{
		string[] parts = line.Split(':')[1].Split(',');
		return new Point<long>(long.Parse(parts[0].Trim()[2..]), long.Parse(parts[1].Trim()[2..]));
	}
}