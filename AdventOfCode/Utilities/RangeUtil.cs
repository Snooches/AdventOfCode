using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities;

public class RangeUtil
{
	public static IEnumerable<(long, long)> SortAndReduce(IEnumerable<(long, long)> ranges)
	{
		IList<(long, long)> result = [];
		(long Min, long Max)? currentRange = null;
		foreach ((long min, long max) in ranges.OrderBy(x => x.Item1))
		{
			if (currentRange is not null && currentRange.Value.Max < min - 1)
			{
				result.Add(currentRange.Value);
				currentRange = null;
			}
			if (currentRange is null)
			{
				currentRange = (min, max);
				continue;
			}
			currentRange = (currentRange.Value.Min, Math.Max(currentRange.Value.Max, max));
		}
		if (currentRange is not null)
			result.Add(currentRange.Value);
		return result;
	}
}