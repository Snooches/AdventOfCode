using Utilities.Interfaces;

namespace AoC2018.Day01;

internal class InputDataConverter : IInputDataConverter<IEnumerable<int>>
{
	public IEnumerable<int> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			yield return int.Parse(line);
		}
	}
}