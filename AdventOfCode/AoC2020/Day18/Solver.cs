using AoC2020.Day18.Tokens;
using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day18;

public class Solver : AbstractSolver<IEnumerable<string>, long, long>
{
	protected override string SolutionTextA => $"The Sum of all Terms with no OperatorPrecedence is {SolutionValueA}.";
	protected override string SolutionTextB => $"The Sum of all Terms with OperatorPrecedence + before * is {SolutionValueB}.";

	public Solver(IInputDataConverter<IEnumerable<string>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		SolutionValueA = SolutionValueB = 0;
		foreach (string line in inputData)
		{
			IToken[] tokenList = Tokenizer.Tokenize(line).ToArray();
			SolutionValueA += TermEvaluator.Evaluate(tokenList, EvaluationDirection.LeftToRight, OperatorPrecedence.None);
			SolutionValueB += TermEvaluator.Evaluate(tokenList, EvaluationDirection.LeftToRight, OperatorPrecedence.LineBeforePoint);
		}
	}
}