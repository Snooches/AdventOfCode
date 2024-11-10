using Utilities.Interfaces;

namespace AoC2023.Day04;

internal class InputDataConverter : IInputDataConverter<IEnumerable<Scratchcard>>
{
	public IEnumerable<Scratchcard> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			Scratchcard card = new(line[10..39].Split(' ', StringSplitOptions.RemoveEmptyEntries)
											   .Select(x => int.Parse(x)).ToHashSet());
			foreach (int number in line[42..].Split(' ', StringSplitOptions.RemoveEmptyEntries)
											.Select(x => int.Parse(x)))
				card.PickNumber(number);
			yield return card;
		}
	}
}