using System.Text.RegularExpressions;
using Utilities.Interfaces;

namespace AoC2023.Day02;

internal partial class InputDataConverter : IInputDataConverter<IEnumerable<Game>>
{
	private static readonly Regex gameRegex = GameRegex();
	private static readonly Regex revealRegex = RevealRegex();

	public IEnumerable<Game> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			Match match = gameRegex.Match(line);
			if (match.Success)
			{
				Game game = new(int.Parse(match.Groups["GameId"].Value));
				foreach (String revealStr in match.Groups["Reveals"].Value.Split(';'))
				{
					Match revealMatch = revealRegex.Match(revealStr);
					Reveal reveal = new(int.TryParse(revealMatch.Groups["Red"].Value, out int red) ? red : 0,
										int.TryParse(revealMatch.Groups["Green"].Value, out int green) ? green : 0,
										int.TryParse(revealMatch.Groups["Blue"].Value, out int blue) ? blue : 0);
					game.Reveals.Add(reveal);
				}
				yield return game;
			}
		}
	}

	[GeneratedRegex("Game (?<GameId>\\d+):(?<Reveals>.+)")]
	private static partial Regex GameRegex();

	[GeneratedRegex("(?:(?:(?<Blue>\\d+) blue,? ?)|(?:(?<Red>\\d+) red,? ?)|(?:(?<Green>\\d+) green,? ?))+")]
	private static partial Regex RevealRegex();
}