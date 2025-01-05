using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day12;

internal class Solver(IInputDataConverter<Dictionary<Point<int>, char>> inputDataConverter, IFileReader fileReader)
	: AbstractSolver<Dictionary<Point<int>, char>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	private List<Vector<int>> directions = [new(0, 1), new(1, 0), new(0, -1), new(-1, 0)];

	protected override void SolveImplemented()
	{
		HashSet<Point<int>> visited = [];
		HashSet<HashSet<Point<int>>> areas = [];

		int x = 0, y = 0;
		while (inputData.ContainsKey(new Point<int>(x, y)))
		{
			while (inputData.ContainsKey(new Point<int>(x, y)))
			{
				if (!visited.Contains(new Point<int>(x, y)))
				{
					HashSet<Point<int>> area = GetArea(x, y);
					areas.Add(area);
					visited.UnionWith(area);
				}
				x++;
			}
			x = 0;
			y++;
		}
		SolutionValueA = areas.Sum(GetFenceValueSimple);
		SolutionValueB = areas.Sum(GetFenceValueComplex);
	}

	private long GetFenceValueSimple(HashSet<Point<int>> area)
	{
		return area.Sum(point => directions.Count(direction => !area.Contains(point + direction))) * area.Count;
	}

	private long GetFenceValueComplex(HashSet<Point<int>> area)
	{
		int corners = 0;
		HashSet<Point<int>> neighbourCollection = [];
		
		foreach(Point<int> plot in area)
			for (int i = 1; i <= 4; i++)
			{
				if(!area.Contains(plot + directions[i - 1]))
					neighbourCollection.Add(plot+directions[i - 1]);
				if (!area.Contains(plot + directions[i - 1]) &&
						!area.Contains(plot + directions[i % 4]))
					corners++;
			}
		
		foreach (Point<int> outsideNeighbour in neighbourCollection)
			for (int i = 1; i <= 4; i++)
				if(area.Contains(outsideNeighbour+directions[i-1]) && 
					 area.Contains(outsideNeighbour+directions[i%4]) && 
					 area.Contains(outsideNeighbour+directions[i-1]+directions[i%4]))
					corners++;
			
		return corners * area.Count; //number of corners is equal to number of straight edges
	}

	private HashSet<Point<int>> GetArea(int x, int y)
	{
		HashSet<Point<int>> area = [];
		Queue<Point<int>> toCheck = [];
		toCheck.Enqueue(new Point<int>(x, y));
		while (toCheck.Count > 0)
		{
			Point<int> p = toCheck.Dequeue();
			area.Add(p);
			foreach (Vector<int> direction in directions)
				if (inputData.TryGetValue(p + direction, out char neighbour) && neighbour == inputData[p] && !area.Contains(p + direction) &&
						!toCheck.Contains(p + direction))
					toCheck.Enqueue(p + direction);
		}
		return area;
	}
}