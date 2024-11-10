using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day10;

internal class Solver(IInputDataConverter<char[,]> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<char[,], int, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The number of steps needed to reach the furthest position from the start is: {SolutionValueA}";
	protected override string SolutionTextB => $"The number of enclosed tiles is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		(int x, int y) startingLocation = GetStartingLocation();
		((int x, int y) currentLocation, Direction comingFrom) = GetNextLocation(startingLocation, Direction.South);
		int loopLength = 1;
		while (currentLocation != startingLocation)
		{
			(currentLocation, comingFrom) = GetNextLocation(currentLocation, comingFrom);
			loopLength++;
		}
		SolutionValueA = loopLength / 2;

		IList<(int, int)> possibleLocations = GetAllLocations();
		SolutionValueB = 0;
		while (possibleLocations.Count > 0)
		{
			SolutionValueB += CheckArea(possibleLocations);
		}
	}

	private (int, int) GetStartingLocation()
	{
		for (int y = 0; y < inputData.GetLength(0); y++)
			for (int x = 0; x < inputData.GetLength(1); x++)
				if (inputData[x, y] == 'S')
					return (x, y);
		return (-1, -1);
	}

	private ((int, int), Direction) GetNextLocation((int x, int y) location, Direction from)
	{
		if (from != Direction.North && IsConnected(inputData[location.x, location.y], Direction.North) &&
			IsInBounds(location.x, location.y - 1) && IsConnected(inputData[location.x, location.y - 1], Direction.South))
			return ((location.x, location.y - 1), Direction.South);
		else if (from != Direction.South && IsConnected(inputData[location.x, location.y], Direction.South) &&
			IsInBounds(location.x, location.y + 1) && IsConnected(inputData[location.x, location.y + 1], Direction.North))
			return ((location.x, location.y + 1), Direction.North);
		else if (from != Direction.West && IsConnected(inputData[location.x, location.y], Direction.West) &&
			IsInBounds(location.x - 1, location.y) && IsConnected(inputData[location.x - 1, location.y], Direction.East))
			return ((location.x - 1, location.y), Direction.East);
		else if (from != Direction.East && IsConnected(inputData[location.x, location.y], Direction.East) &&
			IsInBounds(location.x + 1, location.y) && IsConnected(inputData[location.x + 1, location.y], Direction.West))
			return ((location.x + 1, location.y), Direction.West);
		else
			throw new InvalidOperationException("Cant find next location");
	}

	private static bool IsConnected(char pipe, Direction direction)
	{
		return pipe switch
		{
			'S' => true,
			'-' => direction is Direction.East or Direction.West,
			'|' => direction is Direction.North or Direction.South,
			'7' => direction is Direction.West or Direction.South,
			'J' => direction is Direction.West or Direction.North,
			'L' => direction is Direction.North or Direction.East,
			'F' => direction is Direction.East or Direction.South,
			_ => false
		};
	}

	private bool IsInBounds(int x, int y)
	{
		return x >= 0 && x < inputData.GetLength(0)
			&& y >= 0 && y < inputData.GetLength(1);
	}

	private IList<(int, int)> GetAllLocations()
	{
		IList<(int, int)> result = [];
		for (int y = 0; y < inputData.GetLength(1); y++)
			for (int x = 0; x < inputData.GetLength(0); x++)
				result.Add((x, y));
		return result;
	}

	private static int CheckArea(IList<(int, int)> locations)
	{
		int areaSize = 0;
		IList<(int, int)> neighbours = [locations[0]];
		while (neighbours.Count > 0)
		{
			(int, int) neighbour = neighbours[0];
			neighbours.Remove(neighbour);
			locations.Remove(neighbour);
		}
		return 0; //Fix
	}

	private static IList<(int, int)> GetNeighbours((int x, int y) location)
	{
		return [(location.x - 1, location.y),
				(location.x + 1, location.y),
				(location.x, location.y - 1),
				(location.x, location.y + 1)];
	}

	private enum Direction
	{
		North,
		East,
		South,
		West
	}
}