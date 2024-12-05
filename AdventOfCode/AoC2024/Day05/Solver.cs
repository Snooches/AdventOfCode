using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day05;

internal class Solver(IInputDataConverter<(Dictionary<int, HashSet<int>>, HashSet<int[]>)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(Dictionary<int, HashSet<int>> Rules, HashSet<int[]> Updates), long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The sum of the middle page numbers of all valid updates is: {SolutionValueA}";
	protected override string SolutionTextB => $"The sum of the middle page numbers of all reordered invalid updates is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		foreach (int[] update in inputData.Updates)
		{
			int middlePageOfValidUpdate = GetMiddlePageIfValid(update);
			SolutionValueA += middlePageOfValidUpdate;
			if (middlePageOfValidUpdate == 0)
				SolutionValueB += GetMiddlePageIfInvalid(update);
		}
	}

	private int GetMiddlePageIfValid(int[] update)
	{
		HashSet<int> printedPages = [];
		foreach (int page in update)
			if (inputData.Rules.TryGetValue(page, out HashSet<int>? succesors) &&
				succesors.Intersect(printedPages).Any())
				return 0;
			else
				printedPages.Add(page);
		return update[(update.Length -1)/2];
	}

	private int GetMiddlePageIfInvalid(int[] update)
	{
		List<int> printedPages = [];
		foreach(int page in update)
			if(!inputData.Rules.TryGetValue(page, out HashSet<int>? successors))
				printedPages.Add(page);
			else
				for (int i = 0;i<= printedPages.Count; i++)
					if (!printedPages[..^i].Intersect(successors).Any())
					{
						printedPages.Insert(printedPages.Count-i, page);
						break;
					}
		return printedPages[(printedPages.Count - 1)/2];
	}
}