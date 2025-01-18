using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day17;

internal class Solver(IInputDataConverter<DebuggerConfiguration> inputDataConverter, IFileReader fileReader)
	: AbstractSolver<DebuggerConfiguration, string?, int?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = String.Join(',', new OpCodeDebugger(inputData).GetOutput());
		List<byte> program = inputData.Program.ToList();
		for (int i = 0; i < 512; i++)
		{
			if (i % 10000000 == 0) Console.WriteLine(i);
			IEnumerable<byte> output = new OpCodeDebugger(inputData with { RegisterA = i }).GetOutput();
			Console.WriteLine($"{i:d3}: {String.Join(',', output)}");
			if (!ProgramsAreEqual(program, output))
				continue;
			SolutionValueB = i;
			break;
		}
	}

	private static bool ProgramsAreEqual(List<byte> program, IEnumerable<byte> output)
	{
		int i = 0;
		foreach (byte operation in output)
		{
			if (program[i] != operation)
				return false;
			i++;
		}
		return i == program.Count;
	}
}