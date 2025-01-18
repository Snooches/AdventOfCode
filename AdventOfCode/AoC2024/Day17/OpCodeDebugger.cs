namespace AoC2024.Day17;

public class OpCodeDebugger(DebuggerConfiguration config)
{
	private int registerA = config.RegisterA;
	private int registerB = config.RegisterB;
	private int registerC = config.RegisterC;
	private readonly List<(OpCode Code, byte Parameter)> program = ParseProgram(config.Program).ToList();

	private int instructionPointer;
	private byte? output;

	public IEnumerable<byte> GetOutput()
	{
		while (instructionPointer < program.Count)
		{
			while (instructionPointer < program.Count && output == null)
			{
				ProcessOperation();
			}
			if (output is null) yield break;
			yield return output.Value;
			output = null;
		}
	}

	private void ProcessOperation()
	{
		(OpCode command, byte parameter) = program[instructionPointer];
		switch (command)
		{
			case OpCode.DivideA:
				registerA = (int)(registerA / Math.Pow(2, GetComboParam(parameter)));
			break;
			case OpCode.BitwiseXorWithLiteral:
				registerB ^= program[instructionPointer].Parameter;
			break;
			case OpCode.Modulo8:
				registerB = GetComboParam(parameter) % 8;
			break;
			case OpCode.JumpIfZero:
				instructionPointer = registerA == 0 ? instructionPointer : (parameter / 2) - 1;
			break;
			case OpCode.BitwiseXorWithC:
				registerB ^= registerC;
			break;
			case OpCode.Output:
				output = (byte)(GetComboParam(parameter) % 8);
			break;
			case OpCode.DivideB:
				registerB = (int)(registerA / Math.Pow(2, GetComboParam(parameter)));
			break;
			case OpCode.DivideC:
				registerC = (int)(registerA / Math.Pow(2, GetComboParam(parameter)));
			break;
			default:
				throw new InvalidOperationException($"Unknown OpCode: {command}");
		}
		instructionPointer++;
	}

	private int GetComboParam(byte param)
	{
		return param switch
		{
			0 or 1 or 2 or 3 => param,
			4 => registerA,
			5 => registerB,
			6 => registerC,
			_ => throw new InvalidOperationException($"Invalid ComboParameter {param}."),
		};
	}
	
	private static IEnumerable<(OpCode, byte)> ParseProgram(IEnumerable<byte> program)
	{
		OpCode? lastCode = null;
		foreach (byte input in program)
			if (lastCode is null)
				lastCode = (OpCode)input;
			else
			{
				yield return (lastCode.Value, input);
				lastCode = null;
			}
	}
}