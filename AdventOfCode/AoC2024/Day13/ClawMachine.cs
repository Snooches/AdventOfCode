namespace AoC2024.Day13;

using Utilities;

public record ClawMachine
{
	public required Vector<int> ButtonA { get; init; }
	public required Vector<int> ButtonB { get; init; }
	public required Point<long> Prize { get; init; }
}