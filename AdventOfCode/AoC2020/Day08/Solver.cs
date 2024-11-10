using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day08;

public class Solver : AbstractSolver<IEnumerable<(BootCoderCommand, int)>, int?, int?>
{
	protected override string SolutionTextA => $"Before entering the infinite loop the value of the accumulator was: {SolutionValueA}.";
	protected override string SolutionTextB => $"After swapping the Command in line {commandCounter} the program terminates with the accumulator value {SolutionValueB}";

	private int commandCounter;

	public Solver(IInputDataConverter<IEnumerable<(BootCoderCommand, int)>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
		coder = new BootCoder(inputData.ToList());
	}

	private readonly BootCoder coder;

	protected override void SolveImplemented()
	{
		List<(BootCoderCommand, int)> program = inputData.ToList();
		coder.Run();
		SolutionValueA = !coder.Terminated ? coder.Accumulator : null;

		commandCounter = 0;
		coder.Reset();
		while (!coder.Terminated && commandCounter < program.Count)
		{
			if (program[commandCounter].Item1 == BootCoderCommand.Accumulate)
			{
				commandCounter++;
				continue;
			}
			if (program[commandCounter].Item1 == BootCoderCommand.Jump)
				program[commandCounter] = (BootCoderCommand.NoOperation, program[commandCounter].Item2);
			else if (program[commandCounter].Item1 == BootCoderCommand.NoOperation)
				program[commandCounter] = (BootCoderCommand.Jump, program[commandCounter].Item2);
			coder.SetProgram(program);
			coder.Run();
			if (program[commandCounter].Item1 == BootCoderCommand.Jump)
				program[commandCounter] = (BootCoderCommand.NoOperation, program[commandCounter].Item2);
			else if (program[commandCounter].Item1 == BootCoderCommand.NoOperation)
				program[commandCounter] = (BootCoderCommand.Jump, program[commandCounter].Item2);

			commandCounter++;
		}
		SolutionValueB = coder.Terminated ? coder.Accumulator : null;
	}
}