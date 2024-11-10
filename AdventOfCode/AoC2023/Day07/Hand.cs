namespace AoC2023.Day07;

internal class Hand(char[] cards, int bet)
{
	private readonly byte[] cards = cards.Select(x => ConvertCardValue(x)).ToArray();
	public int Bet { get; } = bet;

	public int CalculateScore(bool variant = false)
	{
		if (cards.Length < 5) return 0;
		int score = variant ? GetHandTypeVariant() : GetHandType();
		for (int i = 0; i < 5; i++)
		{
			score *= 15;
			score += cards[i] == 11 && variant ? 1 : cards[i];
		}
		return score;
	}

	private int GetHandType()
	{
		Dictionary<byte, int> hist = [];
		foreach (byte c in cards)
		{
			if (hist.TryGetValue(c, out int value))
				hist[c] = value + 1;
			else
				hist[c] = 1;
		}
		if (hist.Values.Any(x => x == 5))
			return 6; //Five-Of-A-Kind
		else if (hist.Values.Any(x => x == 4))
			return 5; //Four-Of-A-Kind
		else if (hist.Values.Any(x => x == 3) && hist.Values.Any(x => x == 2))
			return 4; // FUll-House
		else if (hist.Values.Any(x => x == 3))
			return 3; //Three-Of-A-Kind
		else if (hist.Values.Where(x => x == 2).Count() == 2)
			return 2; //TwoPair
		else if (hist.Values.Any(x => x == 2))
			return 1; //Pair
		else
			return 0;
	}

	private int GetHandTypeVariant()
	{
		Dictionary<byte, int> hist = [];
		int jokers = 0;
		foreach (byte c in cards)
		{
			if (c == 11)
				jokers++;
			else if (hist.TryGetValue(c, out int value))
				hist[c] = value + 1;
			else
				hist[c] = 1;
		}
		if (hist.Values.Any(x => x == 5 - jokers) || jokers == 5)
			return 6;
		else if (hist.Values.Any(x => x == 4 - jokers))
			return 5;
		else if (hist.Values.Any(x => x == 3) && hist.Values.Any(x => x == 2)
			|| hist.Values.Where(x => x == 2).Count() == 2 && jokers == 1)
			return 4;
		else if (hist.Values.Any(x => x == 3 - jokers))
			return 3;
		else if (hist.Values.Where(x => x == 2).Count() == 2)
			return 2;
		else if (hist.Values.Any(x => x == 2 - jokers))
			return 1;
		else
			return 0;
	}

	private static byte ConvertCardValue(char c)
	{
		if (byte.TryParse(c.ToString(), out byte value))
			return value;
		return c switch
		{
			'T' => 10,
			'J' => 11,
			'Q' => 12,
			'K' => 13,
			'A' => 14,
			_ => 0
		};
	}
}