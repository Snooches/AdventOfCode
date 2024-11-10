using Utilities.Interfaces;

namespace AoC2020.Day15;

public class InputDataConverter : IInputDataConverter<IEnumerable<int>>
{
	public IEnumerable<int> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
			foreach (string number in line.Split(','))
				if (Int32.TryParse(number, out int result))
					yield return result;
	}
}