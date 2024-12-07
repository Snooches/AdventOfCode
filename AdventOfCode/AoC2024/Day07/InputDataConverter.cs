using Utilities.Interfaces;

namespace AoC2024.Day07;

internal class InputDataConverter : IInputDataConverter<IEnumerable<Equation>>
{
	public IEnumerable<Equation> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			string[] split = line.Split(": ");
			long target = long.Parse(split[0]);
			List<int> opaerands = split[1].Split(' ').Select(x => int.Parse(x)).ToList();
			yield return new Equation(target, opaerands);
		}
	}
}