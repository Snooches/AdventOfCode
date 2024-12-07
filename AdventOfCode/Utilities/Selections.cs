namespace Utilities;

public static class Selections
{
	public static IEnumerable<List<T>> GetCombinations<T>(int count, HashSet<T> items)
	{
		if (count == 0)
			yield return [];
		else
			foreach (List<T> sublist in GetCombinations<T>(count - 1, items))
				foreach (T item in items)
					yield return [item, .. sublist];
	}
}