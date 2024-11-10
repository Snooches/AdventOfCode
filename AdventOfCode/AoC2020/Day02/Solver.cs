using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day02;

public class Solver : AbstractSolver<IEnumerable<PasswordRecord>, int, int>
{
	protected override string SolutionTextA => $"{SolutionValueA} valid records have been found considering ruleset A.";
	protected override string SolutionTextB => $"{SolutionValueB} valid records have been found considering ruleset B.";

	public Solver(IInputDataConverter<IEnumerable<PasswordRecord>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		foreach (PasswordRecord record in inputData)
		{
			if (IsValidRecordByRulesetA(record))
				SolutionValueA++;
			if (IsValidRecordByRulesetB(record))
				SolutionValueB++;
		}
	}

	private static bool IsValidRecordByRulesetA(PasswordRecord record)
	{
		int count = record.Password.Count(c => c == record.PolicyCharacter);
		return count >= record.MinOccurences && count <= record.MaxOccurences;
	}

	private static bool IsValidRecordByRulesetB(PasswordRecord record) =>
		record.MinOccurences >= 1 && record.MinOccurences <= record.Password.Length &&
		record.MaxOccurences >= 1 && record.MaxOccurences <= record.Password.Length &&
		HasLowerCharacterMatch(record) != HasUpperCharacterMatch(record);

	private static bool HasLowerCharacterMatch(PasswordRecord record) => record.Password[record.MinOccurences - 1] == record.PolicyCharacter;

	private static bool HasUpperCharacterMatch(PasswordRecord record) => record.Password[record.MaxOccurences - 1] == record.PolicyCharacter;
}