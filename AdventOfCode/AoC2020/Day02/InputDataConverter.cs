using System.Text.RegularExpressions;
using Utilities.Interfaces;

namespace AoC2020.Day02;

public class InputDataConverter : IInputDataConverter<IEnumerable<PasswordRecord>>
{
	private readonly Regex ValidateForm = new(@"^\d+-\d+ [a-z]: [a-z]*$");
	private readonly Regex FindOccurences = new(@"\d+");
	private readonly Regex FindStrings = new(@"[a-z]+");

	public IEnumerable<PasswordRecord> ConvertInputData(IFileReader fileReader)
	{
		foreach (string inputLine in fileReader.ReadLines())
		{
			if (ValidateForm.Match(inputLine).Success)
			{
				MatchCollection OccurenceValues = FindOccurences.Matches(inputLine);
				MatchCollection StringValues = FindStrings.Matches(inputLine);
				yield return new PasswordRecord()
				{
					MinOccurences = int.Parse(OccurenceValues.ElementAt(0).Value),
					MaxOccurences = int.Parse(OccurenceValues.ElementAt(1).Value),
					PolicyCharacter = StringValues.ElementAt(0).Value.First(),
					Password = StringValues.Count > 1 ? StringValues.ElementAt(1).Value : ""
				};
			}
		}
	}
}