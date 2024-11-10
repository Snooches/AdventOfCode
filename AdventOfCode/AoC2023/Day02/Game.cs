namespace AoC2023.Day02;

internal class Game(int id)
{
	public int Id { get; } = id;
	public IList<Reveal> Reveals { get; } = [];

	public int MinimumSetPower
	{
		get
		{
			(int Red, int Green, int Blue) minSet = Reveals.Select(r => (r.Red, r.Green, r.Blue))
														   .Aggregate((0, 0, 0), ((int, int, int) orig, (int, int, int) act) => (Math.Max(orig.Item1, act.Item1),
																																 Math.Max(orig.Item2, act.Item2),
																																 Math.Max(orig.Item3, act.Item3)));
			return minSet.Red * minSet.Green * minSet.Blue;
		}
	}
}