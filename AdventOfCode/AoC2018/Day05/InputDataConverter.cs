using Utilities.Interfaces;

namespace AoC2018.Day05;

internal class InputDataConverter : IInputDataConverter<string>
{
	public string ConvertInputData(IFileReader fileReader)
	{
		return fileReader.ReadLines().First();
	}
}