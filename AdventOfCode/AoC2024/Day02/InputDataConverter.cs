using Utilities.Interfaces;

namespace AoC2024.Day02;

internal class InputDataConverter : IInputDataConverter<IEnumerable<int[]>>
{
	public IEnumerable<int[]> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			yield return line.Split(' ').Select(x=>int.Parse(x)).ToArray();
		}
	}
}