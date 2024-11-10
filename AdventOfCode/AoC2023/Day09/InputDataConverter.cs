using Utilities.Interfaces;

namespace AoC2023.Day09;

internal class InputDataConverter : IInputDataConverter<IEnumerable<long[]>>
{
	public IEnumerable<long[]> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			yield return line.Split(' ').Select(x => long.Parse(x)).ToArray();
		}
	}
}