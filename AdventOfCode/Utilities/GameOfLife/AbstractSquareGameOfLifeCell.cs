namespace Utilities.GameOfLife;

public abstract class AbstractSquareGameOfLifeCell<MetaDataType> : AbstractGameOfLifeCell<MetaDataType>
{
	public override string Key
	{
		get => $"{x}-{y}";
		init
		{
			string[] split = value.Split("-");
			try
			{
				x = Int32.Parse(split[0]);
				y = Int32.Parse(split[1]);
			}
			catch (Exception)
			{
				throw new ArgumentException("invalid key string");
			}
		}
	}

	protected int x;
	protected int y;

	public override IEnumerable<string> NeighbouringKeys
	{
		get
		{
			for (int xOffset = -1; xOffset <= 1; xOffset++)
				for (int yOffset = -1; yOffset <= 1; yOffset++)
					if (xOffset != 0 || yOffset != 0)
						yield return $"{x + xOffset}-{y + yOffset}";
		}
	}
}