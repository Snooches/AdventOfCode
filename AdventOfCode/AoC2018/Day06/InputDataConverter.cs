using Utilities.Interfaces;

namespace AoC2018.Day06;

internal class InputDataConverter : IInputDataConverter<IEnumerable<Location>>
{
	public IEnumerable<Location> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			String[] split = line.Split(", ");
			yield return new Location(int.Parse(split[0]), int.Parse(split[1]));
		}
	}
}