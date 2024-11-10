using Utilities;
using Utilities.GameOfLife;
using Utilities.Interfaces;

namespace AoC2020.Day11;

public class Solver : AbstractSolver<IEnumerable<(int, int, char)>, int, int>
{
	protected override string SolutionTextA =>
		$"There are {SolutionValueA} occupied Seats after the situation stabilizes.";

	protected override string SolutionTextB => $"After considering not only adjacent Seats, there are {SolutionValueB} occupied Seats after the situation stabilizes.";

	public Solver(IInputDataConverter<IEnumerable<(int, int, char)>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		List<(int x, int y, char content)> input = inputData.ToList();
		if (input.Count == 0)
		{
			SolutionValueA = SolutionValueB = 0;
			return;
		}
		GameOfLifeController<Seat, SeatMetaData> controllerA = new();
		GameOfLifeController<SeatB, SeatMetaData> controllerB = new();
		SeatMetaData metaData = new()
		{
			MinX = input.Select(((int x, int, char) space) => space.x).Min(),
			MaxX = input.Select(((int x, int, char) space) => space.x).Max(),
			MinY = input.Select(((int, int y, char) space) => space.y).Min(),
			MaxY = input.Select(((int, int y, char) space) => space.y).Max(),
		};
		int[] reviveRange = { 0 };
		int[] keepAliveRangeA = Enumerable.Range(0, 4).ToArray();
		int[] keepAliveRangeB = Enumerable.Range(0, 5).ToArray();
		foreach ((int x, int y, char content) in input)
		{
			switch (content)
			{
				case 'L':
					controllerA.AddCell(new Seat() { Key = $"{x}-{y}", MetaData = metaData, KeepAliveRange = keepAliveRangeA, ReviveRange = reviveRange });
					controllerB.AddCell(new SeatB() { Key = $"{x}-{y}", MetaData = metaData, KeepAliveRange = keepAliveRangeB, ReviveRange = reviveRange });
					break;

				case '#':
					controllerA.AddCell(new Seat() { Key = $"{x}-{y}", IsAlive = true, MetaData = metaData, KeepAliveRange = keepAliveRangeA, ReviveRange = reviveRange });
					controllerB.AddCell(new SeatB() { Key = $"{x}-{y}", IsAlive = true, MetaData = metaData, KeepAliveRange = keepAliveRangeB, ReviveRange = reviveRange });
					break;

				case '.':
					metaData.BlockedCells.Add((x, y));
					break;
			}
		}
		while (!controllerA.IsStabilized)
			controllerA.Step();
		SolutionValueA = controllerA.LivingCellCount;

		while (!controllerB.IsStabilized)
			controllerB.Step();
		SolutionValueB = controllerB.LivingCellCount;
	}
}