using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day15;

public class Solver : AbstractSolver<IEnumerable<int>, int, int>
{
	protected override string SolutionTextA => $"The {SolutionATargetTurn}th number will be {SolutionValueA}.";
	protected override string SolutionTextB => $"The {SolutionBTargetTurn}th number will be {SolutionValueB}.";

	public int SolutionATargetTurn { get; set; } = 2020;
	public int SolutionBTargetTurn { get; set; } = 30000000;

	public Solver(IInputDataConverter<IEnumerable<int>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	private Dictionary<int, int> pastNumbers = new();
	private int calledNumber;

	protected override void SolveImplemented()
	{
		Init();
		for (int i = inputData.Count() + 1; i <= SolutionBTargetTurn; i++)
		{
			int numberToCallThisTurn = pastNumbers.ContainsKey(calledNumber) ? (i - 1) - pastNumbers[calledNumber] : 0;
			pastNumbers[calledNumber] = (i - 1);
			calledNumber = numberToCallThisTurn;
			if (i == SolutionATargetTurn)
				SolutionValueA = calledNumber;
		}
		SolutionValueB = calledNumber;
	}

	private void Init()
	{
		pastNumbers = new(Math.Max(SolutionATargetTurn, SolutionBTargetTurn));
		List<int> inputList = inputData.ToList();
		for (int i = 1; i <= inputData.Count(); i++)
		{
			calledNumber = inputList[i - 1];
			if (i < inputData.Count())
				pastNumbers[calledNumber] = i;
		}
	}
}