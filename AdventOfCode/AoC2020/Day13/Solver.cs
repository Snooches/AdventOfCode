using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day13;

public class Solver : AbstractSolver<(int, IEnumerable<int>), int?, long?>
{
	protected override string SolutionTextA => $"The product of wait time and the bus to use is: {SolutionValueA}";
	protected override string SolutionTextB => $"At time {SolutionValueB} all busses arrive in the required intervalls.";

	public Solver(IInputDataConverter<(int, IEnumerable<int>)> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		int currentTime = inputData.Item1;
		List<int> busIds = inputData.Item2.ToList();
		int waitTime = int.MaxValue;
		foreach (int id in busIds.Where((id) => id > 0))
		{
			int specificWaitTime = id - (currentTime % id);
			if (specificWaitTime < waitTime)
			{
				waitTime = specificWaitTime;
				SolutionValueA = waitTime * id;
			}
		}

		long firstOccurence = 0;
		long StepToNextOccurence = 1;
		for (int i = 0; i < busIds.Count(); i++)
		{
			if (busIds[i] == 0)
				continue;
			List<long> seenOffsets = new();
			while ((busIds[i] - (firstOccurence % busIds[i])) % busIds[i] != (i % busIds[i]))
			{
				firstOccurence += StepToNextOccurence;
				long offset = firstOccurence % busIds[i];
				if (seenOffsets.Contains(offset))
				{
					SolutionValueB = null;
					return;
				}
				seenOffsets.Add(offset);
			}
			StepToNextOccurence = lcm(StepToNextOccurence, busIds[i]);
		}
		if (busIds.Count() > 0)
			SolutionValueB = firstOccurence;
	}

	private static long gcf(long a, long b)
	{
		while (b != 0)
		{
			long temp = b;
			b = a % b;
			a = temp;
		}
		return a;
	}

	private static long lcm(long a, long b)
	{
		return (a / gcf(a, b)) * b;
	}
}