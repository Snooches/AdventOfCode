using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day16;

internal class Solver(IInputDataConverter<Dictionary<Point<int>, GridSpace>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<Dictionary<Point<int>, GridSpace>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		Point<int> start = inputData.First(x => x.Value == GridSpace.Start).Key;
		Point<int> finish = inputData.First(x => x.Value == GridSpace.Finish).Key;
		long? lowestCostToFinish = null;
		HashSet<Point<int>> pointsOnLowCostRoute = [];
		Vector<int> startDirection = new(1, 0); //east
		Dictionary<(Point<int>, Vector<int>), int> visited = [];
		visited[(start, startDirection)] = 0;
		Queue<(Point<int>, Vector<int>, int, HashSet<Point<int>>)> currentBlock = [];
		currentBlock.Enqueue((start, startDirection, 0, [start]));
		Queue<(Point<int>, Vector<int>, int, HashSet<Point<int>>)> nextBlock = [];

		while (true)
		{
			while (currentBlock.Count > 0)
			{
				(Point<int> position, Vector<int> direction, int cost, HashSet<Point<int>> route) = currentBlock.Dequeue();
				if (position == finish)
				{
					if (cost < lowestCostToFinish)
					{
						lowestCostToFinish = cost;
						pointsOnLowCostRoute = route;
					}
					else if (cost == lowestCostToFinish)
					{
						pointsOnLowCostRoute.UnionWith(route);
					}
					continue;
				}
				Point<int> targetLocation = position + direction;
				if (inputData[targetLocation] is GridSpace.Finish or GridSpace.Empty && (!visited.TryGetValue((targetLocation,direction), out int targetCost) || targetCost >= cost+1))
				{
					currentBlock.Enqueue((targetLocation,direction, cost+1, route.Union([targetLocation]).ToHashSet()));
				}
			}
			
			currentBlock = nextBlock;
			nextBlock = [];
		}
	}

	private static IEnumerable<Vector<int>> GetOrthogonals(Vector<int> direction)
	{
		if (direction.X == 0)
		{
			yield return new(1, 0);
			yield return new(-1, 0);
		}
		else
		{
			yield return new(0, 1);
			yield return new(0, -1);
		}
	}
}