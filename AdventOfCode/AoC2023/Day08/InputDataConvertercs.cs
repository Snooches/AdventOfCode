using Utilities.Interfaces;

namespace AoC2023.Day08;

internal class InputDataConverter : IInputDataConverter<(char[], Dictionary<string, (string, string)>)>
{
	public (char[], Dictionary<string, (string, string)>) ConvertInputData(IFileReader fileReader)
	{
		string[] lines = fileReader.ReadLines().ToArray();

		char[] instructions = lines[0].ToCharArray();
		Dictionary<string, (string, string)> nodes = [];
		foreach (string line in lines[2..])
		{
			nodes[line[..3]] = (line[7..10], line[12..15]);
		}
		return (instructions, nodes);
	}
}