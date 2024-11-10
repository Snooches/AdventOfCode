using Utilities.Interfaces;

namespace AoC2020.Day04;

public class InputDataConverter : IInputDataConverter<IEnumerable<Dictionary<Identifier, string>>>
{
	public IEnumerable<Dictionary<Identifier, string>> ConvertInputData(IFileReader fileReader)
	{
		Dictionary<Identifier, string> passport = new();
		foreach (string line in fileReader.ReadLines())
		{
			if (line == "")
			{
				yield return passport;
				passport = new();
				continue;
			}

			foreach (string attribute in line.Split(' '))
			{
				AddAttribute(passport, attribute.Split(':', 2));
			}
		}
		if (passport.Keys.Count > 0)
			yield return passport;
	}

	private static void AddAttribute(Dictionary<Identifier, string> passport, string[] keyValuePair)
	{
		if (keyValuePair.Length != 2)
			return;
		switch (keyValuePair[0])
		{
			case "byr":
				passport.Add(Identifier.BirthYear, keyValuePair[1]);
				break;

			case "iyr":
				passport.Add(Identifier.IssueYear, keyValuePair[1]);
				break;

			case "eyr":
				passport.Add(Identifier.ExpirationYear, keyValuePair[1]);
				break;

			case "hgt":
				passport.Add(Identifier.Height, keyValuePair[1]);
				break;

			case "hcl":
				passport.Add(Identifier.HairColor, keyValuePair[1]);
				break;

			case "ecl":
				passport.Add(Identifier.EyeColor, keyValuePair[1]);
				break;

			case "pid":
				passport.Add(Identifier.PassportID, keyValuePair[1]);
				break;

			case "cid":
				passport.Add(Identifier.CountryID, keyValuePair[1]);
				break;
		}
	}
}