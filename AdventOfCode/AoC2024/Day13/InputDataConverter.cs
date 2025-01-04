using Utilities.Interfaces;

namespace AoC2024.Day13;

using Utilities;

internal class InputDataConverter : IInputDataConverter<IEnumerable<ClawMachine>>
{
	public IEnumerable<ClawMachine> ConvertInputData(IFileReader fileReader)
	{
		Vector ButtonA = new(0, 0);
		Vector ButtonB = new(0, 0);
		Point Prize = new(0, 0);
		foreach (string line in fileReader.ReadLines())
			switch (line.Split(':')[0])
			{
				case "Button A":
					ButtonA = ParseVector(line);
				break;
				case "Button B":
					ButtonB = ParseVector(line);
				break;
				case "Prize":
					Prize = ParsePrize(line);
					yield return new ClawMachine()
											 {
												 ButtonA = ButtonA,
												 ButtonB = ButtonB,
												 Prize = Prize,
											 };
				break;
			}
	}

	private Vector ParseVector(string line)
	{
		string[] parts = line.Split(':')[1].Split(',');
		return new Vector(int.Parse(parts[0].Trim()[2..]), int.Parse(parts[1].Trim()[2..]));
	}

	private Point ParsePrize(string line)
	{
		string[] parts = line.Split(':')[1].Split(',');
		return new Point(int.Parse(parts[0].Trim()[2..]), int.Parse(parts[1].Trim()[2..]));
	}
}