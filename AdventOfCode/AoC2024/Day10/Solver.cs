using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day10;

internal class Solver(IInputDataConverter<Dictionary<Point<int>, byte>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<Dictionary<Point<int>, byte>, int?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	private static readonly IEnumerable<Vector<int>> directions = [new(-1, 0), new(1, 0), new(0, -1), new(0, 1)];

	protected override void SolveImplemented()
	{
		SolutionValueA = 0;
		foreach (Point<int> p in inputData.Keys.Where(x => inputData[x] == 0))
		{
			HashSet<Point<int>> visited = [p];
			Queue<Point<int>> routesToContinue = new([p]);
			while (routesToContinue.Count > 0)
			{
				Point<int> location = routesToContinue.Dequeue();
				if (inputData[location] == 9)
					continue;
				foreach (Vector<int> v in directions)
				{
					Point<int> nextLocation = location + v;
					if (visited.Contains(nextLocation))
						continue;
					if (inputData.TryGetValue(nextLocation, out var elevation) && inputData[location] + 1 == elevation)
					{
						routesToContinue.Enqueue(nextLocation);
						visited.Add(nextLocation);
					}
				}
			}
			SolutionValueA += visited.Count(x => inputData[x] == 9);
		}

		SolutionValueB = 0;
		foreach (Point<int> p in inputData.Keys.Where(x => inputData[x] == 0))
		{
			Queue<Point<int>> routesToContinue = new([p]);
			while (routesToContinue.Count > 0)
			{
				Point<int> location = routesToContinue.Dequeue();
				if (inputData[location] == 9)
				{
					SolutionValueB++;
					continue;
				}
				foreach (Vector<int> v in directions)
				{
					Point<int> nextLocation = location + v;
					if (inputData.TryGetValue(nextLocation, out var elevation) && inputData[location] + 1 == elevation)
						routesToContinue.Enqueue(nextLocation);
				}
			}
		}
	}
}