using Utilities.Interfaces;

namespace AoC2020.Day22;

public class InputDataConverter : IInputDataConverter<(IEnumerable<byte>, IEnumerable<byte>)>
{
	public (IEnumerable<byte>, IEnumerable<byte>) ConvertInputData(IFileReader fileReader)
	{
		List<byte> deck1 = new();
		List<byte> deck2 = new();
		bool secondDeck = false;
		foreach (string line in fileReader.ReadLines())
		{
			if (String.IsNullOrWhiteSpace(line))
				secondDeck = true;
			if (Byte.TryParse(line, out byte value))
				if (secondDeck)
					deck2.Add(value);
				else
					deck1.Add(value);
		}
		return (deck1, deck2);
	}
}