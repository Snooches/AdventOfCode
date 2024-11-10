using Utilities.Interfaces;

namespace AoC2023.Day01;

internal class InputDataConverter : IInputDataConverter<IEnumerable<string>>
{
	public IEnumerable<string> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			yield return line;
		}
	}
}