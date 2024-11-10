namespace AoC2020.Day20;

public class Pattern
{
	public char[,] Grid { get; }
	public int Width => Grid.GetLength(0);
	public int Height => Grid.GetLength(1);

	public Pattern(IList<string> input)
	{
		Grid = new char[input[0].Length, input.Count];
		int y = 0;
		foreach (string line in input)
		{
			int x = 0;
			foreach (char c in line)
			{
				if (x >= Grid.GetLength(0))
					throw new Exception("Invalid input!");
				if (y >= Grid.GetLength(1))
					throw new Exception("Invalid input!");
				Grid[x, y] = c;
				x++;
			}
			y++;
		}
	}

	public Pattern(char[,] grid)
	{
		Grid = grid;
	}

	public virtual IEnumerable<Pattern> GetVariants()
	{
		yield return this;
		yield return this.RotateLeft();
		yield return this.RotateRight();
		yield return this.Rotate180();
		yield return this.FlipBottmRightToTopLeft();
		yield return this.FlipBottomLeftToTopRight();
		yield return this.FlipLefToRight();
		yield return this.FlipTopToBottom();
	}

	public Pattern RotateRight()
	{
		return new Pattern(Grid.RotateRight());
	}

	public Pattern RotateLeft()
	{
		return new Pattern(Grid.RotateLeft());
	}

	public Pattern Rotate180()
	{
		return new Pattern(Grid.Rotate180());
	}

	public Pattern FlipTopToBottom()
	{
		return new Pattern(Grid.FlipTopToBottom());
	}

	public Pattern FlipLefToRight()
	{
		return new Pattern(Grid.FlipLefToRight());
	}

	public Pattern FlipBottomLeftToTopRight()
	{
		return new Pattern(Grid.FlipBottomLeftToTopRight());
	}

	public Pattern FlipBottmRightToTopLeft()
	{
		return new Pattern(Grid.FlipBottmRightToTopLeft());
	}
}