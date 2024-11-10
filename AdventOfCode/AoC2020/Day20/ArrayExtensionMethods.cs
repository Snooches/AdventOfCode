namespace AoC2020.Day20;

public static class ArrayExtensionMethods
{
	public static char[,] RotateRight(this char[,] grid)
	{
		char[,] newGrid = new char[grid.GetLength(1), grid.GetLength(0)];
		for (int x = 0; x < grid.GetLength(1); x++)
			for (int y = 0; y < grid.GetLength(0); y++)
				newGrid[x, y] = grid[y, (grid.GetLength(1) - 1 - x)];
		return newGrid;
	}

	public static char[,] RotateLeft(this char[,] grid)
	{
		char[,] newGrid = new char[grid.GetLength(1), grid.GetLength(0)];
		for (int x = 0; x < grid.GetLength(1); x++)
			for (int y = 0; y < grid.GetLength(0); y++)
				newGrid[x, y] = grid[(grid.GetLength(0) - 1 - y), x];
		return newGrid;
	}

	public static char[,] Rotate180(this char[,] grid)
	{
		char[,] newGrid = new char[grid.GetLength(0), grid.GetLength(1)];
		for (int x = 0; x < grid.GetLength(0); x++)
			for (int y = 0; y < grid.GetLength(1); y++)
				newGrid[x, y] = grid[(grid.GetLength(0) - 1 - x), (grid.GetLength(1) - 1 - y)];
		return newGrid;
	}

	public static char[,] FlipTopToBottom(this char[,] grid)
	{
		char[,] newGrid = new char[grid.GetLength(0), grid.GetLength(1)];
		for (int x = 0; x < grid.GetLength(0); x++)
			for (int y = 0; y < grid.GetLength(1); y++)
				newGrid[x, y] = grid[x, (grid.GetLength(1) - 1 - y)];
		return newGrid;
	}

	public static char[,] FlipLefToRight(this char[,] grid)
	{
		char[,] newGrid = new char[grid.GetLength(0), grid.GetLength(1)];
		for (int x = 0; x < grid.GetLength(0); x++)
			for (int y = 0; y < grid.GetLength(1); y++)
				newGrid[x, y] = grid[(grid.GetLength(0) - 1 - x), y];
		return newGrid;
	}

	public static char[,] FlipBottomLeftToTopRight(this char[,] grid)
	{
		char[,] newGrid = new char[grid.GetLength(1), grid.GetLength(0)];
		for (int x = 0; x < grid.GetLength(1); x++)
			for (int y = 0; y < grid.GetLength(0); y++)
				newGrid[x, y] = grid[y, x];
		return newGrid;
	}

	public static char[,] FlipBottmRightToTopLeft(this char[,] grid)
	{
		char[,] newGrid = new char[grid.GetLength(1), grid.GetLength(0)];
		for (int x = 0; x < grid.GetLength(1); x++)
			for (int y = 0; y < grid.GetLength(0); y++)
				newGrid[x, y] = grid[(grid.GetLength(0) - 1 - y), (grid.GetLength(1) - 1 - x)];
		return newGrid;
	}
}