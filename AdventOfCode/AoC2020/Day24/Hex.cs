namespace AoC2020.Day24;

public record Hex
{
	public int East { get; set; }
	public int NorthEast { get; set; }
	public int SouthEast { get; set; }

	public void Reduce()
	{
		while (NorthEast > 0 && SouthEast > 0)
		{
			NorthEast--;
			SouthEast--;
			East++;
		}
		while (NorthEast < 0 && SouthEast < 0)
		{
			NorthEast++;
			SouthEast++;
			East--;
		}
		while (East > 0 && (NorthEast < 0 || SouthEast < 0))
		{
			East--;
			NorthEast++;
			SouthEast++;
		}
		while (East < 0 && (NorthEast > 0 || SouthEast > 0))
		{
			East++;
			NorthEast--;
			SouthEast--;
		}
	}
}