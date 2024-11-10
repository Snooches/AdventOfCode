namespace AoC2023.Day16;

public class MirrorTile(MirrorTileType type)
{
	private ICollection<Direction> EntryDirections = [];
	private ICollection<Direction> ExitDirections = [];

	public bool IsEnergized => EntryDirections.Count != 0;

	public IEnumerable<Direction> EnterLight(Direction direction)
	{
		IEnumerable<Direction> result;
		EntryDirections.Add(direction);
		IEnumerable<Direction> newExits = GetExitDirections().ToList();
		if (newExits.Count() > ExitDirections.Count)
		{
			result = newExits.Except(ExitDirections);
			ExitDirections = [.. newExits];
			return result;
		}
		return [];
	}

	public void Reset()
	{
		EntryDirections = [];
		ExitDirections = [];
	}

	private IEnumerable<Direction> GetExitDirections()
	{
		if (type == MirrorTileType.Empty && EntryDirections.Contains(Direction.North)
		 || type == MirrorTileType.SplitterVertical && EntryDirections.Contains(Direction.North)
		 || type == MirrorTileType.SplitterVertical && EntryDirections.Contains(Direction.West)
		 || type == MirrorTileType.SplitterVertical && EntryDirections.Contains(Direction.East)
		 || type == MirrorTileType.MirrorTopLeft && EntryDirections.Contains(Direction.West)
		 || type == MirrorTileType.MirrorTopRight && EntryDirections.Contains(Direction.East))
			yield return Direction.North;
		if (type == MirrorTileType.Empty && EntryDirections.Contains(Direction.South)
		 || type == MirrorTileType.SplitterVertical && EntryDirections.Contains(Direction.South)
		 || type == MirrorTileType.SplitterVertical && EntryDirections.Contains(Direction.East)
		 || type == MirrorTileType.SplitterVertical && EntryDirections.Contains(Direction.West)
		 || type == MirrorTileType.MirrorTopLeft && EntryDirections.Contains(Direction.East)
		 || type == MirrorTileType.MirrorTopRight && EntryDirections.Contains(Direction.West))
			yield return Direction.South;
		if (type == MirrorTileType.Empty && EntryDirections.Contains(Direction.East)
		 || type == MirrorTileType.SplitterHorizontal && EntryDirections.Contains(Direction.East)
		 || type == MirrorTileType.SplitterHorizontal && EntryDirections.Contains(Direction.North)
		 || type == MirrorTileType.SplitterHorizontal && EntryDirections.Contains(Direction.South)
		 || type == MirrorTileType.MirrorTopLeft && EntryDirections.Contains(Direction.South)
		 || type == MirrorTileType.MirrorTopRight && EntryDirections.Contains(Direction.North))
			yield return Direction.East;
		if (type == MirrorTileType.Empty && EntryDirections.Contains(Direction.West)
		 || type == MirrorTileType.SplitterHorizontal && EntryDirections.Contains(Direction.West)
		 || type == MirrorTileType.SplitterHorizontal && EntryDirections.Contains(Direction.North)
		 || type == MirrorTileType.SplitterHorizontal && EntryDirections.Contains(Direction.South)
		 || type == MirrorTileType.MirrorTopLeft && EntryDirections.Contains(Direction.North)
		 || type == MirrorTileType.MirrorTopRight && EntryDirections.Contains(Direction.South))
			yield return Direction.West;
	}
}

public enum MirrorTileType
{
	Empty,
	SplitterVertical,
	SplitterHorizontal,
	MirrorTopLeft,
	MirrorTopRight
}

public enum Direction
{
	North,
	West,
	East,
	South
}