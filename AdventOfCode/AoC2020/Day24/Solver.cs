using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day24;

public class Solver : AbstractSolver<IEnumerable<Hex>, int, int>
{
	protected override string SolutionTextA => $"{SolutionValueA}";

	//public override string SolutionB => "Solution has not been implemented yet";
	public Solver(IInputDataConverter<IEnumerable<Hex>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader) { }

	private List<Hex> blackHexes = new();

	protected override void SolveImplemented()
	{
		foreach (Hex hex in inputData)
		{
			if (blackHexes.Contains(hex))
				blackHexes.Remove(hex);
			else
				blackHexes.Add(hex);
		}
		SolutionValueA = blackHexes.Count();
	}
}