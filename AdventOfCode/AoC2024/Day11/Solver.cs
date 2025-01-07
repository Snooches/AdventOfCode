using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day11;

using System.Numerics;

internal class Solver(IInputDataConverter<IEnumerable<long>> inputDataConverter, IFileReader fileReader)
	: AbstractSolver<IEnumerable<long>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	private Dictionary<long, long> stoneCounts = [];
	private Dictionary<long, List<long>> blinkSteps = [];

	protected override void SolveImplemented()
	{
		blinkSteps[0] = [1];
		foreach (long stone in inputData)
			if (!stoneCounts.TryAdd(stone, 1))
				stoneCounts[stone]++;
		Blink(25);
		SolutionValueA = stoneCounts.Values.Sum();
		Blink(50);
		SolutionValueB = stoneCounts.Values.Sum();
	}

	private void Blink(int cycles)
	{
		for (int i = 0; i < cycles; i++)
		{
			Dictionary<long, long> newCounts = [];
			foreach (long parent in stoneCounts.Keys)
			foreach (long child in GetChildren(parent))
				if (!newCounts.TryAdd(child, stoneCounts[parent]))
					newCounts[child] += stoneCounts[parent];
			stoneCounts = newCounts;
		}
	}

	private List<long> GetChildren(long parent)
	{
		if (blinkSteps.TryGetValue(parent, out List<long>? children))
			return children;

		int length = (int)(BigInteger.Log10(parent) + 1);
		if (length % 2 == 0)
		{
			(BigInteger first, BigInteger second) = BigInteger.DivRem(parent, BigInteger.Pow(10, length / 2));
			children = [(long)first, (long)second];
		}
		else
		{
			children = [parent * 2024];
		}
		blinkSteps[parent] = children;
		return children;
	}
}