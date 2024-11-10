namespace AoC2020.Day05;

public record Seat
{
	public int Row { get; set; }
	public int Column { get; set; }
	public int Id
	{
		get => Row * 8 + Column;
		set
		{
			Row = value / 8;
			Column = value % 8;
		}
	}
}