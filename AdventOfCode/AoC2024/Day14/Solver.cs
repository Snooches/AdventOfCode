using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day14;

internal class Solver(IInputDataConverter<IEnumerable<SecurityBot>> inputDataConverter, IFileReader fileReader)
	: AbstractSolver<IEnumerable<SecurityBot>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	private const int LengthX = 101;
	private const int ThresholdX = (LengthX - 1) / 2;
	private const int LengthY = 103;
	private const int ThresholdY = (LengthY - 1) / 2;

	protected override void SolveImplemented()
	{
		int q1 = 0, q2 = 0, q3 = 0, q4 = 0;
		foreach (SecurityBot bot in inputData)
		{
			Point<int> endPosition = bot.Position + bot.Velocity * 100;
			int endX = endPosition.X % LengthX;
			endX += endX < 0 ? LengthX : 0;
			int endY = endPosition.Y % LengthY;
			endY += endY < 0 ? LengthY : 0;
			if (endX < ThresholdX && endY < ThresholdY)
				q1++;
			else if (endX > ThresholdX && endY < ThresholdY)
				q2++;
			else if (endX < ThresholdX && endY > ThresholdY)
				q3++;
			else if (endX > ThresholdX && endY > ThresholdY)
				q4++;
		}
		SolutionValueA = q1 * q2 * q3 * q4;

		List<SecurityBot> bots = inputData.ToList();
		List<(int Steps, int Spread)> xDistribution = [];
		List<(int Steps, int Spread)> yDistribution = [];
		for (int i = 0; i < Math.Max(LengthX, LengthY); i++)
		{
			xDistribution.Add((i, CalculateSpread(bots.Select(x => x.Position.X).ToList())));
			yDistribution.Add((i, CalculateSpread(bots.Select(x => x.Position.Y).ToList())));
			foreach (SecurityBot bot in bots)
			{
				bot.Move();
				bot.ModuloPosition(LengthX, LengthY);
			}
		}
		int xLoopCount = xDistribution.First(x=>x.Spread == xDistribution.Min(y=>y.Spread)).Steps;
		int yLoopCount = yDistribution.First(x=>x.Spread == yDistribution.Min(y=>y.Spread)).Steps;
		while (xLoopCount != yLoopCount)
			if (xLoopCount < yLoopCount)
				xLoopCount += LengthX;
			else
				yLoopCount += LengthY;
		SolutionValueB = xLoopCount;
	}

	private int CalculateSpread(List<int> positions)
	{
		int spread = 0;
		for (int i = 0; i < positions.Count - 1; i++)
		for (int j = i + 1; j < positions.Count; j++)
			spread += Math.Abs(positions[i] - positions[j]);
		return spread;
	}
}