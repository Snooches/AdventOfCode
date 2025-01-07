using Utilities.Interfaces;

namespace AoC2024.Day11;

internal class InputDataConverter : IInputDataConverter<IEnumerable<long>>
{
	public IEnumerable<long> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		foreach (string word in line.Split(' '))
			if (long.TryParse(word, out long number))
				yield return number;
	}
}