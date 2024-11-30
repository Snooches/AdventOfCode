using Utilities;
using Utilities.Interfaces;

namespace AoC2018.Day06;

internal class Solver(IInputDataConverter<IEnumerable<Location>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<Location>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		(int, int)[] directions = [(-1, 0), (0, -1), (0, 1), (1, 0)];
		int minX = int.MaxValue;
		int maxX = int.MinValue;
		int minY = int.MaxValue;
		int maxY = int.MinValue;

		Dictionary<Location, (Location?, int)> mappedLocations = [];
		int currentDistance = 0;
		Dictionary<Location, Location?> locationsToProcess = [];
		Dictionary<Location, Location?> nextPhase = [];
		foreach (Location loc in inputData)
		{
			locationsToProcess.Add(loc, loc);
			minX = Math.Min(minX, loc.X);
			minY = Math.Min(minY, loc.Y);
			maxX = Math.Max(maxX, loc.X);
			maxY = Math.Max(maxY, loc.Y);
		}

		while (locationsToProcess.Count > 0)
		{
			foreach (KeyValuePair<Location, Location?> kvp in locationsToProcess)
			{
				if (!mappedLocations.TryAdd(kvp.Key, (kvp.Value, currentDistance)))
					continue;
				foreach((int xDif, int yDif) in directions)
				{
					Location possibleNextLocation = new(kvp.Key.X + xDif, kvp.Key.Y + yDif);
					if (possibleNextLocation.X < minX || possibleNextLocation.X > maxX
						|| possibleNextLocation.Y < minY || possibleNextLocation.Y > maxY
						|| mappedLocations.ContainsKey(possibleNextLocation))
						continue;
					if (nextPhase.ContainsKey(possibleNextLocation) && !nextPhase[possibleNextLocation].Equals(kvp.Value))
						nextPhase[possibleNextLocation] = null;
					else
						nextPhase[possibleNextLocation] = kvp.Value;
				}
			}

			currentDistance++;

			locationsToProcess = nextPhase;
			nextPhase = [];

			//Visualize(mappedLocations, minX, maxX, minY, maxY);
			//Console.ReadKey();
		}

		HashSet<(int, int)> disqualifiedOrigins = [];
		Dictionary<(int, int), int> areaSizes = [];

		//Visualize(mappedLocations, minX, maxX, minY, maxY);

		foreach (Location origin in mappedLocations.Values.Select(x => x.Item1).Where(x => x is not null))
		{
			if (disqualifiedOrigins.Contains((origin.X, origin.Y)))
				continue;
			if (origin.X == minX || origin.X == maxX
			 || origin.Y == minY || origin.Y == maxY)
			{
				disqualifiedOrigins.Add((origin.X, origin.Y));
				areaSizes.Remove((origin.X, origin.Y));
				continue;
			}
			if (areaSizes.ContainsKey((origin.X, origin.Y)))
				areaSizes[(origin.X, origin.Y)]++;
			else
				areaSizes[(origin.X, origin.Y)] = 1;
		}

		SolutionValueA = areaSizes.Values.Max();
	}

	private void Visualize(Dictionary<Location, (Location?, int)> locations, int minX, int maxX, int minY, int maxY)
	{
		for (int y = minY; y <= maxY; y++)
		{
			for (int x = minX; x <= maxX; x++)
			{
				if(!locations.ContainsKey(new(x, y)))
				{
					Console.Write(".");
					continue;
				}
				if (locations[new(x, y)].Item1 is null)
				{
					Console.Write(".");
					continue;
				}
				Location origin = locations[new(x, y)].Item1!.Value;
				char designation = (char)((origin.X * 31 + origin.Y) % 26 + 97);
				Console.Write((origin.X == x && origin.Y == y) ? (char)(designation - 32) : designation);
			}
			Console.WriteLine();
		}
		Console.WriteLine();
		Console.WriteLine();
	}
}