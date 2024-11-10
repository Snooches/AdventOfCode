using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day05;

internal class Solver(IInputDataConverter<(IEnumerable<long>, IEnumerable<SeedMapper>)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(IEnumerable<long>, IEnumerable<SeedMapper>), long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The lowest matching location number is: {SolutionValueA}";
	protected override string SolutionTextB => $"The lowest matching location when considering ranges is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = inputData.Item1.Select(x => mapToEnd(x)).Min();
		IList<(long, long)> seedRanges = [];
		for (int i = 0; i < inputData.Item1.Count(); i += 2)
		{
			long start = inputData.Item1.ElementAt(i);
			seedRanges.Add((start, start + inputData.Item1.ElementAt(i + 1) - 1));
		}
		IEnumerable<(long, long)> locations = mapToEnd(seedRanges);
		SolutionValueB = locations.First().Item1;
	}

	private long mapToEnd(long input)
	{
		long value = input;
		foreach (SeedMapper mapper in inputData.Item2)
		{
			value = mapper.Map(value);
		}
		return value;
	}

	private IEnumerable<(long, long)> mapToEnd(IEnumerable<(long, long)> input)
	{
		IEnumerable<(long, long)> values = input;
		foreach (SeedMapper mapper in inputData.Item2)
		{
			RangeUtil.SortAndReduce(values);
			values = mapper.Map(values);
		}
		return RangeUtil.SortAndReduce(values);
	}
}