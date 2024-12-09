using Utilities.Interfaces;

namespace AoC2024.Day09;

internal class InputDataConverter : IInputDataConverter<IEnumerable<int>>
{
	public IEnumerable<int> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
			foreach(char c in line)
			{
				yield return c - 48;
			}
	}
}