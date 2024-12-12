using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day11;

internal class Solver(IInputDataConverter<List<long>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<List<long>, int?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";


	private readonly Dictionary<long, List<long>> calculatedValues = [];
	protected override void SolveImplemented()
	{
		for (int i = 0; i < 75; i++)
		{
			Console.WriteLine($"After Round {i} - {calculatedValues.Count} calculated - {inputData.Count} Values in list");
			Blink(inputData);
		}
		SolutionValueA = inputData.Count;
	}

	private void Blink(List<long> stones)
	{
		for (int i = stones.Count - 1; i >= 0; i--)
		{
			if (stones[i] == 0)
			{
				stones[i] = 1;
				continue;
			}
			if (calculatedValues.TryGetValue(stones[i], out List<long>? nextValue))
			{
				stones[i] = nextValue[0];
				if (nextValue.Count > 1)
					stones.Insert(i + 1, nextValue[1]);
				continue;
			}
			String stringValue = stones[i].ToString();
			if (stringValue.Length % 2 == 0)
			{
				long key = stones[i];
				stones[i] = long.Parse(stringValue[(stringValue.Length / 2)..]);
				stones.Insert(i, long.Parse(stringValue[..(stringValue.Length / 2)]));
				calculatedValues[key] = [stones[i], stones[i + 1]];
				continue;
			}
			calculatedValues.Add(stones[i], [stones[i] * 2024]);
			stones[i] = calculatedValues[stones[i]][0];
		}
	}
}