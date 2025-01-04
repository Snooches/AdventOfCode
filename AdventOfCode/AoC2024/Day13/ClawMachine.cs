namespace AoC2024.Day13;

using Utilities;

public record ClawMachine
{
	public required Vector ButtonA { get; init; }
	public required Vector ButtonB { get; init; }
	public required Point Prize { get; init; }
}