using Utilities.Interfaces;

namespace AoC2024.Day17;

internal class InputDataConverter : IInputDataConverter<DebuggerConfiguration>
{
	public DebuggerConfiguration ConvertInputData(IFileReader fileReader)
	{
		List<string> lines = fileReader.ReadLines().ToList();
		return new DebuggerConfiguration(
			int.Parse(lines[0].Split(':')[1].Trim()),
			int.Parse(lines[1].Split(':')[1].Trim()),
			int.Parse(lines[2].Split(':')[1].Trim()),
			lines[4].Split(':')[1].Split(',').Select(byte.Parse));
	}
}