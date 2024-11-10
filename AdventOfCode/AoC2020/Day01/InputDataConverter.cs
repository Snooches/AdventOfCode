using Utilities.Interfaces;

namespace AoC2020.Day01;

public class InputDataConverter : IInputDataConverter<IEnumerable<int>>
{
	public IEnumerable<int> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
			if (int.TryParse(line, out int result))
				yield return result;
	}
}