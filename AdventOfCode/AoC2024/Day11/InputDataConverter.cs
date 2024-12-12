using Utilities.Interfaces;

namespace AoC2024.Day11;

internal class InputDataConverter : IInputDataConverter<List<long>>
{
	public List<long> ConvertInputData(IFileReader fileReader)
	{
		return fileReader.ReadLines().First().Split(" ").Select(long.Parse).ToList();
	}
}