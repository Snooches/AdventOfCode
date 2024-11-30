namespace AoC2018.Day07;
public record ProcessStep(char Id)
{
	public HashSet<ProcessStep> Predecessors { get; } = [];
	public HashSet<ProcessStep> Successors { get; } = [];
}