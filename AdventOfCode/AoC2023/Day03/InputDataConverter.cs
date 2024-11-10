using Utilities.Interfaces;

namespace AoC2023.Day03;

internal class InputDataConverter : IInputDataConverter<char[][]>
{
	public char[][] ConvertInputData(IFileReader fileReader)
	{
		return fileReader.ReadLines().Select(l => l.ToCharArray()).ToArray();
	}
}