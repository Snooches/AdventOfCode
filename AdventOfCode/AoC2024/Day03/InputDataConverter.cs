using Utilities.Interfaces;

namespace AoC2024.Day03;

internal class InputDataConverter : IInputDataConverter<IEnumerable<string>>
{
	public IEnumerable<string> ConvertInputData(IFileReader fileReader)
	{
		foreach(string line in fileReader.ReadLines())
			yield return line;
	}
}