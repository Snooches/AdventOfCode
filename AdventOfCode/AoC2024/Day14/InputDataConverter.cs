using Utilities.Interfaces;

namespace AoC2024.Day14;

using Utilities;

public class InputDataConverter : IInputDataConverter<IEnumerable<SecurityBot>>
{
	public IEnumerable<SecurityBot> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			string[] attributes = line.Split(' ');
			string[] position = attributes[0][2..].Split(',');
			string[] velocity = attributes[1][2..].Split(',');
			yield return new SecurityBot(new Point<int>(int.Parse(position[0]), int.Parse(position[1])),
																	 new Vector<int>(int.Parse(velocity[0]), int.Parse(velocity[1])));
		}
	}
}