using Utilities.Interfaces;

namespace AoC2020.Day10;

public class InputDataConverter : IInputDataConverter<IEnumerable<int>>
{
	public IEnumerable<int> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
			if (Int32.TryParse(line, out int result))
				yield return result;
	}
}