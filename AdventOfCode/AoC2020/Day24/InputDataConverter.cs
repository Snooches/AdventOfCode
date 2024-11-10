using Utilities.Interfaces;

namespace AoC2020.Day24;

public class InputDataConverter : IInputDataConverter<IEnumerable<Hex>>
{
	public IEnumerable<Hex> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			Hex hex = new();
			string directions = line;
			while (!String.IsNullOrEmpty(directions))
			{
				if (directions[0] == 'e')
				{
					hex.East++;
					directions = new String(directions.ToArray()[1..]);
				}
				else if (directions[0] == 'w')
				{
					hex.East--;
					directions = new String(directions.ToArray()[1..]);
				}
				else if (directions[0] == 'n' && directions[1] == 'e')
				{
					hex.NorthEast++;
					directions = new String(directions.ToArray()[2..]);
				}
				else if (directions[0] == 'n' && directions[1] == 'w')
				{
					hex.SouthEast--;
					directions = new String(directions.ToArray()[2..]);
				}
				else if (directions[0] == 's' && directions[1] == 'e')
				{
					hex.SouthEast++;
					directions = new String(directions.ToArray()[2..]);
				}
				else if (directions[0] == 's' && directions[1] == 'w')
				{
					hex.NorthEast--;
					directions = new String(directions.ToArray()[2..]);
				}
			}
			hex.Reduce();
			yield return hex;
		}
	}
}