namespace AoC2023.Day05;

internal class SeedMapper
{
	private IList<(long, long, long)> mappings = [];

	public void AddMapping(long destinationStart, long sourceStart, long length)
	{
		mappings.Add((sourceStart, sourceStart + length - 1, destinationStart - sourceStart));
	}

	public long Map(long input)
	{
		try
		{
			(long from, long to, long offset) mapping = mappings.First(((long from, long to, long _) m) => m.from <= input && m.to >= input);
			return input + mapping.offset;
		}
		catch (InvalidOperationException)
		{
			return input;
		}
	}

	public IEnumerable<(long, long)> Map(IEnumerable<(long, long)> input)
	{
		mappings = mappings.OrderBy(x => x.Item1).ToList();
		IList<(long, long)> result = [];
		foreach ((long minL, long maxL) in input)
		{
			long min = minL;
			long max = maxL;
			while (min <= max)
			{
				(long From, long To, long offset) mapping = mappings.FirstOrDefault(x => x.Item1 <= min && x.Item2 >= min);
				if (mapping == default)
					mapping = mappings.Where(x => x.Item1 > min).FirstOrDefault();
				if (mapping == default)
				{
					result.Add((min, max));
					break;
				}
				if (mapping.From > min)
				{
					result.Add((min, mapping.From - 1));
					min = mapping.From;
				}
				if (mapping.To < max)
				{
					result.Add((min + mapping.offset, mapping.To + mapping.offset));
					min = mapping.To + 1;
				}
				else
				{
					result.Add((min + mapping.offset, max + mapping.offset));
					min = max + 1;
				}
			}
		}
		return result;
	}
}