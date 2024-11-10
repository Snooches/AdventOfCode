namespace AoC2020.Day23;

public class LoopedList<T>
{
	private List<T> list;

	public LoopedList(IEnumerable<T> input)
	{
		this.list = input.ToList();
	}
}