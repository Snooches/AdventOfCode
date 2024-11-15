namespace AoC2018.Day04;
public record GuardSleepSchedule
{
	public int Id { get; set; }
	public Dictionary<int, int> Schedule { get; set; } = Enumerable.Range(0,60).ToDictionary(x=>x,x=>0);
	public int TotalSleepMinutes => Schedule.Values.Sum();
}