using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day15;

internal class Solver(IInputDataConverter<(Dictionary<Point, StorageContent>, IEnumerable<Vector>)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(Dictionary<Point, StorageContent> Storage, IEnumerable<Vector> Moves), long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The total sum of al box GPS values is: {SolutionValueA}";
	protected override string SolutionTextB => $"The total sum of all boxes GPS valuesin the expanded storage is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		Dictionary<Point, StorageContent> expandedStorage = ExpandStorage(inputData.Storage);
		Point roboLocation = inputData.Storage.First(x => x.Value == StorageContent.Robot).Key;
		foreach (Vector move in inputData.Moves)
		{
			if (EnactMove(inputData.Storage, roboLocation, move))
				roboLocation += move;
		}
		SolutionValueA = CalculateGPS(inputData.Storage);

		roboLocation = expandedStorage.First(x => x.Value == StorageContent.Robot).Key;
		foreach (Vector move in inputData.Moves)
		{
			if (EnactMove(expandedStorage, roboLocation, move))
				roboLocation += move;
		}
		SolutionValueB = CalculateGPS(expandedStorage);
	}

	private static bool EnactMove(Dictionary<Point, StorageContent> storage, Point location, Vector move, bool simulate = false)
	{
		Point target = location + move;
		switch (storage[target])
		{
			case StorageContent.Wall:
				return false;

			case StorageContent.Empty:
				if (!simulate)
				{
					storage[target] = storage[location];
					storage[location] = StorageContent.Empty;
				}
				return true;

			case StorageContent.Box:
				if (EnactMove(storage, target, move))
				{
					storage[target] = storage[location];
					storage[location] = StorageContent.Empty;
					return true;
				}
				else
					return false;

			case StorageContent.LeftBox:
			case StorageContent.RightBox:
				if (move.X == 0) // Vertical Move
				{
					bool invertDirection = storage[target] == StorageContent.RightBox;
					if (EnactMove(storage, target, move, true)
						&& EnactMove(storage, new(target.X + (invertDirection ? -1 : 1), target.Y), move, true))
					{
						if (!simulate)
						{
							EnactMove(storage, target, move);
							EnactMove(storage, new(target.X + (invertDirection ? -1 : 1), target.Y), move);
							storage[target] = storage[location];
							storage[location] = StorageContent.Empty;
						}
						return true;
					}
					return false;
				}
				else // Horizontal Move
				{
					if (EnactMove(storage, target, move))
					{
						if (!simulate)
						{
							storage[target] = storage[location];
							storage[location] = StorageContent.Empty;
						}
						return true;
					}
					else
						return false;
				}
			default:
				return false;
		}
	}

	private static int CalculateGPS(Dictionary<Point, StorageContent> storage)
	{
		return storage
			.Where(x => x.Value is StorageContent.Box or StorageContent.LeftBox)
			.Sum(x => x.Key.X + x.Key.Y * 100);
	}

	private static Dictionary<Point, StorageContent> ExpandStorage(Dictionary<Point, StorageContent> storage)
	{
		Dictionary<Point, StorageContent> result = [];
		foreach (KeyValuePair<Point, StorageContent> kvp in storage)
		{
			switch (kvp.Value)
			{
				case StorageContent.Empty:
					result[new(kvp.Key.X * 2, kvp.Key.Y)] = StorageContent.Empty;
					result[new(kvp.Key.X * 2 + 1, kvp.Key.Y)] = StorageContent.Empty;
					break;

				case StorageContent.Wall:
					result[new(kvp.Key.X * 2, kvp.Key.Y)] = StorageContent.Wall;
					result[new(kvp.Key.X * 2 + 1, kvp.Key.Y)] = StorageContent.Wall;
					break;

				case StorageContent.Box:
					result[new(kvp.Key.X * 2, kvp.Key.Y)] = StorageContent.LeftBox;
					result[new(kvp.Key.X * 2 + 1, kvp.Key.Y)] = StorageContent.RightBox;
					break;

				case StorageContent.Robot:
					result[new(kvp.Key.X * 2, kvp.Key.Y)] = StorageContent.Robot;
					result[new(kvp.Key.X * 2 + 1, kvp.Key.Y)] = StorageContent.Empty;
					break;

				default:
					throw new InvalidOperationException();
			}
		}
		return result;
	}
}