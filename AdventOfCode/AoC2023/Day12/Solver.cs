using System.Data;
using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day12;

internal class Solver(IInputDataConverter<IEnumerable<(char[] Row, IList<int> Groups)>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<(char[] Row, IList<int> Groups)>, int, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The sum of the counts is: {SolutionValueA}";
	protected override string SolutionTextB => $"The sum of possible arrangement counts with unfolded conditions is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = 0;
		foreach ((char[] row, IList<int> groups) in inputData)
		{
			SolutionValueA += CountPossibleArrangements(row, groups);
		}
	}

	private int CountPossibleArrangements(char[] row, IList<int> groups)
	{
		int wildcardCount = row.Count(x => x == '?');
		int arrangements = 0;
		for (int i = 0; i < Math.Pow(2, wildcardCount); i++)
		{
			Queue<char> wildcards = new(Convert.ToString(i, 2).PadLeft(wildcardCount, '0').Select(x => x == '0' ? '.' : '#'));
			char[] arrangement = row.Select(x => x == '?' ? wildcards.Dequeue() : x).ToArray();
			if (IsValid(arrangement, groups))
			{
				arrangements++;
			}
		}
		return arrangements;
	}

	private static bool IsValid(char[] arrangement, IList<int> groups)
	{
		bool inGroup = false;
		int currentGroupSize = 0;
		int groupIndex = 0;
		foreach (char c in arrangement)
		{
			if (c == '#')
			{
				inGroup = true;
				currentGroupSize++;
			}
			else if (inGroup)
			{
				inGroup = false;
				if (groupIndex == groups.Count || groups[groupIndex] != currentGroupSize)
					return false;
				currentGroupSize = 0;
				groupIndex++;
			}
		}
		if (inGroup && groupIndex == groups.Count)
			return false;
		if (inGroup && groups[groupIndex] == currentGroupSize)
			groupIndex++;
		return groupIndex == groups.Count();
	}
}