using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day06;

internal class Solver(IInputDataConverter<((int,int),Dictionary<(int,int),State>)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<((int,int),Dictionary<(int,int),State>), long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The guard visited {SolutionValueA} spaces before leaving the area.";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;

		(int X, int Y) currentPosition = inputData.Item1;
		(int XDif, int YDif) currentDirection = (0, -1);
		Dictionary<(int X, int Y), State> grid = inputData.Item2;

		while (grid.ContainsKey(currentPosition))
		{
			if (grid[currentPosition] == State.Empty)
			{
				grid[currentPosition] = State.Visited;
				SolutionValueA++;
			}

			(int,int) nextPosition = (currentPosition.X + currentDirection.XDif, currentPosition.Y + currentDirection.YDif);
			while (grid.ContainsKey(nextPosition) && grid[nextPosition] == State.Blocked)
			{
				currentDirection = GetNextDirection(currentDirection);
				nextPosition = (currentPosition.X + currentDirection.XDif, currentPosition.Y + currentDirection.YDif);
			}

			if (grid.ContainsKey(nextPosition) && grid[nextPosition] == State.Empty && CheckForLoop(grid, currentPosition, currentDirection))
				SolutionValueB++;

			currentPosition = nextPosition;
		}
	}

	private static (int,int) GetNextDirection((int X,int Y) currentPosition)
	{
		return (-currentPosition.Y, currentPosition.X);
	}

	private static Boolean CheckForLoop(Dictionary<(int,int), State> grid, (int X, int Y) currentPosition, (int XDif, int YDif) currentDirection)
	{
		HashSet<((int, int), (int, int))> visited = [];
		(int,int) newObstacle = (currentPosition.X + currentDirection.XDif,currentPosition.Y + currentDirection.YDif);
		while (grid.ContainsKey(currentPosition))
		{
			if (visited.Contains((currentPosition, currentDirection)))
				return true;
			visited.Add((currentPosition, currentDirection));

			(int, int) nextPosition = (currentPosition.X + currentDirection.XDif, currentPosition.Y + currentDirection.YDif);
			while (nextPosition == newObstacle || (grid.ContainsKey(nextPosition) && grid[nextPosition] == State.Blocked))
			{
				currentDirection = GetNextDirection(currentDirection);
				nextPosition = (currentPosition.X + currentDirection.XDif, currentPosition.Y + currentDirection.YDif);
			}

			currentPosition = nextPosition;
		}
		return false;
	}
}