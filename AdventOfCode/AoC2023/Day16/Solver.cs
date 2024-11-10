using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day16;

internal class Solver(IInputDataConverter<MirrorTile[,]> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<MirrorTile[,], int, int>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The number of energized tiles is: {SolutionValueA}";
	protected override string SolutionTextB => $"The maximum number of energized tiles is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueB = SolutionValueA = GetScore(0, 0, Direction.East);
		for (int y = 0; y < inputData.GetLength(1); y++)
		{
			if (y != 0)
				SolutionValueB = Math.Max(GetScore(0, y, Direction.East), SolutionValueB);
			SolutionValueB = Math.Max(GetScore(inputData.GetLength(0) - 1, y, Direction.West), SolutionValueB);
		}
		for (int x = 0; x < inputData.GetLength(0); x++)
		{
			SolutionValueB = Math.Max(GetScore(x, 0, Direction.South), SolutionValueB);
			SolutionValueB = Math.Max(GetScore(x, inputData.GetLength(1) - 1, Direction.North), SolutionValueB);
		}
	}

	private int GetScore(int startX, int startY, Direction startDirection)
	{
		Queue<(int x, int y, Direction direction)> toDo = new();
		toDo.Enqueue((startX, startY, startDirection));

		while (toDo.Any())
		{
			(int x, int y, Direction direction) = toDo.Dequeue();
			foreach (Direction d in inputData[x, y].EnterLight(direction))
			{
				switch (d)
				{
					case Direction.North:
						if (y > 0)
							toDo.Enqueue((x, y - 1, d));
						break;

					case Direction.South:
						if (y < inputData.GetLength(1) - 1)
							toDo.Enqueue((x, y + 1, d));
						break;

					case Direction.West:
						if (x > 0)
							toDo.Enqueue((x - 1, y, d));
						break;

					case Direction.East:
						if (x < inputData.GetLength(0) - 1)
							toDo.Enqueue((x + 1, y, d));
						break;
				}
			}
		}

		int score = 0;
		for (int y = 0; y < inputData.GetLength(1); y++)
			for (int x = 0; x < inputData.GetLength(0); x++)
			{
				if (inputData[x, y].IsEnergized)
					score++;
				inputData[x, y].Reset();
			}
		return score;
	}
}