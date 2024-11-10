using Utilities.Interfaces;

namespace AoC2020.Day25;

public class InputDataConverter : IInputDataConverter<IEnumerable<int>>
{
	public IEnumerable<int> ConvertInputData(IFileReader fileReader)
	{
		var lines = fileReader.ReadLines().ToArray();
		yield return Int32.Parse(lines[0]);
		yield return Int32.Parse(lines[1]);
	}
}