using Utilities.Interfaces;

namespace AoC2020.Day12;

public class InputDataConverter : IInputDataConverter<IEnumerable<(char, int)>>
{
	public IEnumerable<(char, int)> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
			if (Int32.TryParse(line[1..], out int parameter) && "NESWLRF".Contains(line[0]))
				yield return (line[0], parameter);
	}
}