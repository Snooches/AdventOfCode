using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day14;

public class Solver : AbstractSolver<IEnumerable<(BitMask, long, long)>, long, long>
{
	private Dictionary<long, long> memoryV1 = new();
	private Dictionary<long, long> memoryV2 = new();
	protected override string SolutionTextA => $"The sum of all values in memory after applying the BitMask is {SolutionValueA}.";
	protected override string SolutionTextB => $"The sum of all values in memory after applying the BitMask with floating bit support is {SolutionValueB}.";

	public Solver(IInputDataConverter<IEnumerable<(BitMask, long, long)>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		foreach ((BitMask mask, long address, long value) in inputData)
		{
			memoryV1[address] = Convert.ToInt64(mask.ApplyMask(Convert.ToString(value, 2)), 2);
			foreach (string addressString in mask.ApplyMaskWithFloatingBits(Convert.ToString(address, 2)))
			{
				long adr = Convert.ToInt64(addressString, 2);
				memoryV2[adr] = value;
			}
		}
		SolutionValueA = memoryV1.Values.Sum();
		SolutionValueB = memoryV2.Values.Sum();
	}
}