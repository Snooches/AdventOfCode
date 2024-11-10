namespace AoC2020.Day11;

public class SeatB : Seat
{
	public override IEnumerable<string> NeighbouringKeys
	{
		get
		{
			for (int xOffset = -1; xOffset <= 1; xOffset++)
				for (int yOffset = -1; yOffset <= 1; yOffset++)
					if (xOffset != 0 || yOffset != 0)
					{
						int xNew = x + xOffset;
						int yNew = y + yOffset;
						while (MetaData.BlockedCells.Contains((xNew, yNew)))
						{
							xNew += xOffset;
							yNew += yOffset;
						}
						if (xNew >= MetaData.MinX && xNew <= MetaData.MaxX &&
								yNew >= MetaData.MinY && yNew <= MetaData.MaxY)
							yield return $"{xNew}-{yNew}";
					}
		}
	}
}