using Utilities.Interfaces;

namespace AoC2023.Day12;

internal class InputDataConverter : IInputDataConverter<IEnumerable<(char[], IList<int>)>>
{
	public IEnumerable<(char[], IList<int>)> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			String[] split = line.Split(' ');
			yield return (split[0].ToCharArray(), split[1].Split(',').Select(x => int.Parse(x)).ToList());
		}
	}
}