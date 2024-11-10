using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day12;

public class Solver : AbstractSolver<IEnumerable<(char, int)>, int, int>
{
	protected override string SolutionTextA =>
		$"The Manhattan distance to the calculated destination is {SolutionValueA}.";

	protected override string SolutionTextB => $"The Manhattan distance to the calculated destination using Waypoint Navigation is {SolutionValueB}.";

	public Solver(IInputDataConverter<IEnumerable<(char, int)>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		int x1 = 0;
		int vX1 = 1;
		int y1 = 0;
		int vY1 = 0;
		int x2 = 0;
		int vX2 = 10;
		int y2 = 0;
		int vY2 = 1;
		foreach ((char command, int param) in inputData)
		{
			switch (command)
			{
				case 'N':
					y1 += param;
					vY2 += param;
					break;

				case 'S':
					y1 -= param;
					vY2 -= param;
					break;

				case 'E':
					x1 += param;
					vX2 += param;
					break;

				case 'W':
					x1 -= param;
					vX2 -= param;
					break;

				case 'F':
					x1 += param * vX1;
					y1 += param * vY1;
					x2 += param * vX2;
					y2 += param * vY2;
					break;

				case 'R':
					int rotationRight = param % 360;
					rotationRight = rotationRight < 0 ? rotationRight + 360 : rotationRight;
					for (int i = 0; i < rotationRight / 90; i++)
					{
						int h = vX1;
						vX1 = vY1;
						vY1 = 0 - h;
						h = vX2;
						vX2 = vY2;
						vY2 = 0 - h;
					}
					break;

				case 'L':
					int rotationLeft = param % 360;
					rotationLeft = rotationLeft < 0 ? rotationLeft + 360 : rotationLeft;
					for (int i = 0; i < rotationLeft / 90; i++)
					{
						int h = vX1;
						vX1 = 0 - vY1;
						vY1 = h;
						h = vX2;
						vX2 = 0 - vY2;
						vY2 = h;
					}
					break;
			}
		}
		SolutionValueA = Math.Abs(x1) + Math.Abs(y1);
		SolutionValueB = Math.Abs(x2) + Math.Abs(y2);
	}
}