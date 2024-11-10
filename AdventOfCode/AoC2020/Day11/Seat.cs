using Utilities.GameOfLife;

namespace AoC2020.Day11;

public class Seat : AbstractSquareGameOfLifeCell<SeatMetaData>
{
	public override SeatMetaData MetaData { get; init; } = new();

	public override IEnumerable<string> NeighbouringKeys
	{
		get
		{
			for (int xOffset = -1; xOffset <= 1; xOffset++)
				for (int yOffset = -1; yOffset <= 1; yOffset++)
					if (xOffset != 0 || yOffset != 0)
						if (x + xOffset >= MetaData.MinX && x + xOffset <= MetaData.MaxX &&
								y + yOffset >= MetaData.MinY && y + yOffset <= MetaData.MaxY)
							if (!MetaData.BlockedCells.Contains((x + xOffset, y + yOffset)))
								yield return $"{x + xOffset}-{y + yOffset}";
		}
	}
}

public record SeatMetaData
{
	public int MinX { get; set; }
	public int MaxX { get; set; }
	public int MinY { get; set; }
	public int MaxY { get; set; }
	public List<(int, int)> BlockedCells { get; set; } = [];
}