using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day20;

public class Solver : AbstractSolver<IEnumerable<ImageTile>, long, int>
{
	protected override string SolutionTextA => $"The Product of all corner tile ID is {SolutionValueA}.";
	protected override string SolutionTextB => $"There are {SolutionValueB} '#' in the Image that are not part of any Sea Monster.";

	private readonly int metaGridSize;
	private readonly ImageTile[,] metaGrid;
	private readonly int gridSize;
	private readonly char[,] grid;
	private readonly int tileSize;
	private readonly Dictionary<int, ImageTile> tileBacklog = new();

	public Solver(IInputDataConverter<IEnumerable<ImageTile>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
		metaGridSize = (int)Math.Sqrt(inputData.Count());
		metaGrid = new ImageTile[metaGridSize, metaGridSize];
		foreach (ImageTile tile in inputData)
			tileBacklog.Add(tile.ID, tile);
		if (tileBacklog.Count > 0)
			tileSize = tileBacklog.Values.First().Size;
		else
			tileSize = 0;
		gridSize = metaGridSize * (tileSize - 2);
		grid = new char[gridSize, gridSize];
	}

	protected override void SolveImplemented()
	{
		//Find Corner and place in 0,0
		ImageTile corner = FindTopLeftCorner();
		metaGrid[0, 0] = corner;

		//Fill Rest by finding matching edges
		tileBacklog.Remove(metaGrid[0, 0].ID);
		for (int x = 0; x < metaGridSize; x++)
			for (int y = 0; y < metaGridSize; y++)
			{
				if (x + y == 0)
					continue;
				ImageTile match;
				if (x > 0)
					match = FindMatch(metaGrid[x - 1, y].Right);
				else
					match = FindMatch(metaGrid[x, y - 1].Bottom).FlipBottomLeftToTopRight();
				metaGrid[x, y] = match;
				tileBacklog.Remove(match.ID);
			}

		//Calulate SolutionA
		SolutionValueA = 1;
		SolutionValueA *= metaGrid[0, 0].ID;
		SolutionValueA *= metaGrid[0, metaGridSize - 1].ID;
		SolutionValueA *= metaGrid[metaGridSize - 1, 0].ID;
		SolutionValueA *= metaGrid[metaGridSize - 1, metaGridSize - 1].ID;

		//Build Picture
		for (int x = 0; x < gridSize; x++)
			for (int y = 0; y < gridSize; y++)
				grid[x, y] = metaGrid[x / (tileSize - 2), y / (tileSize - 2)].Grid[x % (tileSize - 2) + 1, y % (tileSize - 2) + 1];

		//Search for and mark patter
		Pattern p = new(new List<String>()
				{ "                  # ",
				  "#    ##    ##    ###",
				  " #  #  #  #  #  #   "});
		foreach (Pattern variant in p.GetVariants())
			MarkPattern(variant);

		//Calculate SolutionB
		SolutionValueB = 0;
		foreach (char c in grid)
			if (c == '#')
				SolutionValueB++;
	}

	private ImageTile FindTopLeftCorner()
	{
		foreach (ImageTile tile in inputData)
		{
			int matchCount = 0;
			bool topMatch = false, bottomMatch = false, leftMatch = false, rightMatch = false;
			string topReverse = new(tile.Top.Reverse().ToArray());
			string bottomReverse = new(tile.Bottom.Reverse().ToArray());
			string leftReverse = new(tile.Left.Reverse().ToArray());
			string rightReverse = new(tile.Right.Reverse().ToArray());
			foreach (ImageTile otherTile in inputData)
			{
				if (matchCount > 2)
					break;
				if (otherTile.ID == tile.ID)
					continue;
				if (!topMatch && (otherTile.Edges.Contains(tile.Top) || otherTile.Edges.Contains(topReverse)))
				{
					topMatch = true;
					matchCount++;
					continue;
				}
				if (!bottomMatch && (otherTile.Edges.Contains(tile.Bottom) || otherTile.Edges.Contains(bottomReverse)))
				{
					bottomMatch = true;
					matchCount++;
					continue;
				}
				if (!leftMatch && (otherTile.Edges.Contains(tile.Left) || otherTile.Edges.Contains(leftReverse)))
				{
					leftMatch = true;
					matchCount++;
					continue;
				}
				if (!rightMatch && (otherTile.Edges.Contains(tile.Right) || otherTile.Edges.Contains(rightReverse)))
				{
					rightMatch = true;
					matchCount++;
					continue;
				}
			}
			if (matchCount > 2)
				continue;
			if (!topMatch && !leftMatch)
				return tile;
			if (!topMatch && !rightMatch)
				return tile.RotateLeft();
			if (!leftMatch && !bottomMatch)
				return tile.RotateRight();
			if (!bottomMatch && !rightMatch)
				return tile.Rotate180();
		}
		throw new Exception("No corner tile found!");
	}

	private ImageTile FindMatch(string edge)
	{
		//return Tile with matching edge on left side
		string reverse = new(edge.Reverse().ToArray());
		foreach (ImageTile tile in tileBacklog.Values)
		{
			if (tile.Left == edge)
				return tile;
			if (tile.Left == reverse)
				return tile.FlipTopToBottom();
			if (tile.Top == edge)
				return tile.FlipBottomLeftToTopRight();
			if (tile.Top == reverse)
				return tile.RotateLeft();
			if (tile.Right == edge)
				return tile.FlipLefToRight();
			if (tile.Right == reverse)
				return tile.Rotate180();
			if (tile.Bottom == edge)
				return tile.RotateRight();
			if (tile.Bottom == reverse)
				return tile.FlipBottomRightToTopLeft();
		}
		throw new Exception("No Match found!");
	}

	private void MarkPattern(Pattern pattern)
	{
		for (int xOffset = 0; xOffset <= grid.GetLength(0) - pattern.Width; xOffset++)
			for (int yOffset = 0; yOffset <= grid.GetLength(1) - pattern.Height; yOffset++)
			{
				bool match = true;
				for (int x = 0; x < pattern.Width; x++)
					for (int y = 0; y < pattern.Height; y++)
						if (pattern.Grid[x, y] == '#' && !"#O".Contains(grid[x + xOffset, y + yOffset]))
							match = false;
				if (match)
					for (int x = 0; x < pattern.Width; x++)
						for (int y = 0; y < pattern.Height; y++)
							if (pattern.Grid[x, y] == '#')
								grid[x + xOffset, y + yOffset] = 'O';
			}
	}
}