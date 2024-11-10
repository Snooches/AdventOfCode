using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day10;

public class Solver : AbstractSolver<IEnumerable<int>, int, long>
{
	private const int range = 3;
	protected override string SolutionTextA => $"There are {Diffs[1]} 1-jolt differences, {Diffs[2]} 2-jolt differences and {Diffs[3]} 3-Jolt differences, which leads to a Solution of {SolutionValueA}.";
	protected override string SolutionTextB => $"There are {SolutionValueB} possible arrangements of the adapters that will work.";

	private Dictionary<int, int> Diffs = new();

	public Solver(IInputDataConverter<IEnumerable<int>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		Diffs.Clear();
		for (int i = 1; i < range; i++)
			Diffs.Add(i, 0);
		Diffs.Add(range, 1);

		int previous = 0;
		List<int> adapters = inputData.ToList();
		adapters.Sort();
		Dictionary<int, long> numberOfPaths = new();
		if (adapters.Count > 0)
			numberOfPaths[adapters[0]] = 1;
		for (int x = 1; x <= range; x++)
			if (adapters.Contains(x))
				numberOfPaths[x] = 1;
		foreach (int current in adapters)
		{
			Diffs[current - previous]++;
			for (int x = 1; x <= range; x++)
			{
				if (adapters.Contains(current + x))
					if (numberOfPaths.ContainsKey(current + x))
						numberOfPaths[current + x] += numberOfPaths[current];
					else
						numberOfPaths[current + x] = numberOfPaths[current];
			}
			previous = current;
		}
		SolutionValueA = Diffs[1] * Diffs[range];
		SolutionValueB = numberOfPaths.Count > 0 ? numberOfPaths.Values.Max() : 1;
	}
}