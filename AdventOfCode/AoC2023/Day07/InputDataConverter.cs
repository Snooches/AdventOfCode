using Utilities.Interfaces;

namespace AoC2023.Day07;

internal class InputDataConverter : IInputDataConverter<IEnumerable<Hand>>
{
	public IEnumerable<Hand> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			string[] split = line.Split(' ');
			yield return new Hand(split[0].ToCharArray(), int.Parse(split[1]));
		}
	}
}