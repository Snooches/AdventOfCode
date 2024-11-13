using System.Security.Claims;
using Utilities;
using Utilities.Interfaces;

namespace AoC2018.Day03;

internal class Solver(IInputDataConverter<IEnumerable<Claim>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<Claim>, int?, int?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		Dictionary<(int, int), byte> claimedPieces = [];

		foreach (Claim claim in base.inputData)
			for (int x = claim.X; x < claim.X+claim.Width;x++)
				for (int y = claim.Y; y < claim.Y+claim.Heigth;y++)
				{
					if (claimedPieces.ContainsKey((x, y)))
						claimedPieces[(x, y)]++;
					else
						claimedPieces[(x, y)] = 1;
				}

		SolutionValueA = claimedPieces.Values.Count(x => x > 1);

		foreach (Claim claim in base.inputData)
		{
			Boolean noOverlap = true;
			for (int x = claim.X; x < claim.X + claim.Width && noOverlap; x++)
				for (int y = claim.Y; y < claim.Y + claim.Heigth; y++)
				{
					if (claimedPieces[(x,y)] > 1)
					{
						noOverlap = false;
						break;
					}
				}
			if (noOverlap)
			{
				SolutionValueB = claim.Id;
				break;
			}
		}
	}
}