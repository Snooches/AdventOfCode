namespace AoC2023.Day04;

internal class Scratchcard(HashSet<int> WinningNumbers)
{
	public int NumberOfMatches { get; private set; } = 0;
	public IEnumerable<int> PickedNumbers { get; private set; } = [];

	public void PickNumber(int number)
	{
		PickedNumbers = PickedNumbers.Append(number);
		if (WinningNumbers.Contains(number))
			NumberOfMatches++;
	}
}