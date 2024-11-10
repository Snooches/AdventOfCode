using System.Text.RegularExpressions;
using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day04;

public class Solver : AbstractSolver<IEnumerable<Dictionary<Identifier, string>>, int, int>
{
	protected override string SolutionTextA => $"{SolutionValueA} in {inputData.Count()} passports are valid.";
	protected override string SolutionTextB => $"{SolutionValueB} in {inputData.Count()} passports are valid considering field validations.";

	private static readonly Regex YearRegEx = new("^\\d{4}$");
	private static readonly Regex HeightRegEx = new("^\\d{2,3}(cm|in)$");
	private static readonly Regex HairColorRegEx = new("^#([0-9]|[a-f]){6}$");
	private static readonly Regex EyeColorRegEx = new("^(amb|blu|brn|gry|grn|hzl|oth)$");
	private static readonly Regex PassportIdRegEx = new("^[0-9]{9}$");

	public Solver(IInputDataConverter<IEnumerable<Dictionary<Identifier, string>>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		SolutionValueA = 0;
		SolutionValueB = 0;
		foreach (Dictionary<Identifier, string> passport in inputData)
		{
			if (ContainsMandatoryFields(passport))
			{
				SolutionValueA++;
				if (FieldContentsAreValid(passport))
					SolutionValueB++;
			}
		}
	}

	private static bool ContainsMandatoryFields(Dictionary<Identifier, string> passport) =>
		passport.ContainsKey(Identifier.BirthYear) &&
		passport.ContainsKey(Identifier.IssueYear) &&
		passport.ContainsKey(Identifier.ExpirationYear) &&
		passport.ContainsKey(Identifier.Height) &&
		passport.ContainsKey(Identifier.HairColor) &&
		passport.ContainsKey(Identifier.EyeColor) &&
		passport.ContainsKey(Identifier.PassportID);

	private static bool FieldContentsAreValid(Dictionary<Identifier, string> passport) =>
		IsValidBirthYear(passport[Identifier.BirthYear]) &&
		IsValidIssueYear(passport[Identifier.IssueYear]) &&
		IsValidExpirationYear(passport[Identifier.ExpirationYear]) &&
		IsValidHeight(passport[Identifier.Height]) &&
		IsValidHairColor(passport[Identifier.HairColor]) &&
		IsValidEyeColor(passport[Identifier.EyeColor]) &&
		IsValidPassportID(passport[Identifier.PassportID]);

	private static bool IsValidBirthYear(string birthYear)
	{
		if (YearRegEx.IsMatch(birthYear))
			if (int.Parse(birthYear) >= 1920 && int.Parse(birthYear) <= 2002)
				return true;
		return false;
	}

	private static bool IsValidIssueYear(string issueYear)
	{
		if (YearRegEx.IsMatch(issueYear))
			if (int.Parse(issueYear) >= 2010 && int.Parse(issueYear) <= 2020)
				return true;
		return false;
	}

	private static bool IsValidExpirationYear(string expirationYear)
	{
		if (YearRegEx.IsMatch(expirationYear))
			if (int.Parse(expirationYear) >= 2020 && int.Parse(expirationYear) <= 2030)
				return true;
		return false;
	}

	private static bool IsValidHeight(string height)
	{
		if (HeightRegEx.IsMatch(height))
		{
			int value = int.Parse(height[..^2]);
			if (height.EndsWith("cm") && value >= 150 && value <= 193)
				return true;
			if (height.EndsWith("in") && value >= 59 && value <= 76)
				return true;
		}
		return false;
	}

	private static bool IsValidHairColor(string hairColor) =>
		HairColorRegEx.IsMatch(hairColor);

	private static bool IsValidEyeColor(string eyeColor) =>
	  EyeColorRegEx.IsMatch(eyeColor);

	private static bool IsValidPassportID(string passportID) =>
		PassportIdRegEx.IsMatch(passportID);
}