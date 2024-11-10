using Utilities.Interfaces;

namespace AoC2020.Day23;

public class InputDataConverter : IInputDataConverter<IEnumerable<int>>
{
	public IEnumerable<int> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
			foreach (char c in line)
				if (Int32.TryParse(c.ToString(), out int result))
					yield return result;
	}
}