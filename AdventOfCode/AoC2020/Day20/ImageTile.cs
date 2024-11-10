namespace AoC2020.Day20;

public class ImageTile
{
	public int ID { get; init; }
	public char[,] Grid { get; }
	public int Size => Grid.GetLength(0);

	public ImageTile(IList<string> lines)
	{
		if (lines.Count == 0)
			throw new Exception("Empty Tile is not valid.");
		if (lines.Any((string line) => line.Length != lines.Count))
			throw new Exception("Tile is not square.");
		Grid = new char[lines.Count, lines.Count];
		for (int x = 0; x < lines.Count; x++)
			for (int y = 0; y < lines.Count; y++)
				Grid[x, y] = lines[y][x];
	}

	public ImageTile(char[,] grid)
	{
		Grid = grid;
	}

	public string Top => GetRow(0);
	public string Right => GetColumn(Size - 1);
	public string Bottom => GetRow(Size - 1);
	public string Left => GetColumn(0);

	public IEnumerable<string> Edges
	{
		get
		{
			yield return GetRow(0);
			yield return GetRow(Size - 1);
			yield return GetColumn(0);
			yield return GetColumn(Size - 1);
		}
	}

	public string GetRow(int row) => new(Enumerable.Range(0, Grid.GetLength(0)).Select(x => Grid[x, row]).ToArray());

	public string GetColumn(int column) => new(Enumerable.Range(0, Grid.GetLength(1)).Select(y => Grid[column, y]).ToArray());

	public IEnumerable<ImageTile> GetVariants()
	{
		yield return this;
		yield return this.RotateRight();
		yield return this.RotateLeft();
		yield return this.Rotate180();
		yield return this.FlipTopToBottom();
		yield return this.FlipLefToRight();
		yield return this.FlipBottomLeftToTopRight();
		yield return this.FlipBottomRightToTopLeft();
	}

	public ImageTile RotateRight()
	{
		return new ImageTile(Grid.RotateRight()) { ID = this.ID };
	}

	public ImageTile RotateLeft()
	{
		return new ImageTile(Grid.RotateLeft()) { ID = this.ID };
	}

	public ImageTile Rotate180()
	{
		return new ImageTile(Grid.Rotate180()) { ID = this.ID };
	}

	public ImageTile FlipTopToBottom()
	{
		return new ImageTile(Grid.FlipTopToBottom()) { ID = this.ID };
	}

	public ImageTile FlipLefToRight()
	{
		return new ImageTile(Grid.FlipLefToRight()) { ID = this.ID };
	}

	public ImageTile FlipBottomLeftToTopRight()
	{
		return new ImageTile(Grid.FlipBottomLeftToTopRight()) { ID = this.ID };
	}

	public ImageTile FlipBottomRightToTopLeft()
	{
		return new ImageTile(Grid.FlipBottmRightToTopLeft()) { ID = this.ID };
	}

	public override bool Equals(object? other)
	{
		if (other is null)
			return false;
		if (other is not ImageTile otherTile)
			return false;
		for (int x = 0; x < Grid.GetLength(0); x++)
			for (int y = 0; y < Grid.GetLength(1); y++)
				if (Grid[x, y] != otherTile.Grid[x, y])
					return false;
		return true;
	}

	public override int GetHashCode()
	{
		int hash = 0;
		foreach (char value in Grid)
			hash &= value.GetHashCode();
		return hash;
	}
}