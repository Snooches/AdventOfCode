namespace AoC2020.Day02;

public record PasswordRecord
{
	public char PolicyCharacter { get; init; }
	public int MinOccurences { get; init; }
	public int MaxOccurences { get; init; }
	public string Password { get; init; } = "";

	public PasswordRecord() { }
	public PasswordRecord(char policyCharacter, int minOccurences, int maxOccurences, string password)
	{
		PolicyCharacter = policyCharacter;
		MinOccurences = minOccurences;
		MaxOccurences = maxOccurences;
		Password = password;
	}
}