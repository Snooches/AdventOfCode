namespace AoC2020.Day08;

public enum BootCoderCommand
{
	NoOperation,
	Accumulate,
	Jump
}

public class BootCoder
{
	public int Accumulator { get; private set; }
	public bool Terminated { get; private set; }

	private int programCounter;
	private List<(BootCoderCommand, int)> program;
	private List<int> runLines = new();

	public BootCoder(List<(BootCoderCommand, int)> program)
	{
		this.program = program.ToList();
	}

	public void SetProgram(List<(BootCoderCommand, int)> program)
	{
		this.program = program;
	}

	public void Run()
	{
		Reset();
		while (!runLines.Contains(programCounter) && programCounter < program.Count)
		{
			runLines.Add(programCounter);
			switch (program[programCounter].Item1)
			{
				case BootCoderCommand.NoOperation:
					programCounter++;
					break;

				case BootCoderCommand.Jump:
					programCounter += program[programCounter].Item2;
					break;

				case BootCoderCommand.Accumulate:
					Accumulator += program[programCounter].Item2;
					programCounter++;
					break;
			}
		}
		Terminated = programCounter == program.Count;
	}

	public void Reset()
	{
		Accumulator = 0;
		Terminated = false;
		programCounter = 0;
		runLines.Clear();
	}
}