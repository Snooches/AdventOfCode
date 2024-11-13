using System.Text.RegularExpressions;
using Utilities.Interfaces;

namespace AoC2018.Day03;

internal partial class InputDataConverter : IInputDataConverter<IEnumerable<Claim>>
{
	private readonly Regex regex = InputLineRegex();

	public IEnumerable<Claim> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			Match match = regex.Match(line);
			yield return new Claim(int.Parse(match.Groups[1].Value),
								   int.Parse(match.Groups[2].Value),
								   int.Parse(match.Groups[3].Value),
								   int.Parse(match.Groups[4].Value),
								   int.Parse(match.Groups[5].Value));
		}
	}

	[GeneratedRegex(@"^#(\d+) @ (\d+),(\d+): (\d+)x(\d+)$")]
	private static partial Regex InputLineRegex();
}