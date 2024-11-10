using Utilities.GameOfLife;

namespace AoC2020.Day17;

public class GameOfLifeCell4D : AbstractGameOfLifeCell<object?>
{
	public override string ToString()
	{ return IsAlive ? "alive" : "dead"; }

	private int x;
	private int y;
	private int z;
	private int w;

	public override string Key
	{
		get => $"{x}/{y}/{z}/{w}";
		init
		{
			string[] split = value.Split("/");
			try
			{
				x = Int32.Parse(split[0]);
				y = Int32.Parse(split[1]);
				z = Int32.Parse(split[2]);
				w = Int32.Parse(split[3]);
			}
			catch (Exception)
			{
				throw new Exception("invalid key string");
			}
		}
	}

	public override object? MetaData { get; init; }

	public override IEnumerable<string> NeighbouringKeys
	{
		get
		{
			for (int xOffset = -1; xOffset <= 1; xOffset++)
				for (int yOffset = -1; yOffset <= 1; yOffset++)
					for (int zOffset = -1; zOffset <= 1; zOffset++)
						for (int wOffset = -1; wOffset <= 1; wOffset++)
							if (xOffset != 0 || yOffset != 0 || zOffset != 0 || wOffset != 0)
								yield return $"{x + xOffset}/{y + yOffset}/{z + zOffset}/{w + wOffset}";
		}
	}
}