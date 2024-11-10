using AoC2020.Day18.Tokens;

namespace AoC2020.Day18;

public class TermEvaluator
{
	public static long Evaluate(IToken[] tokens, EvaluationDirection direction, OperatorPrecedence precedence)
	{
		if (tokens.Length == 1)
			return tokens[0].Evaluate(direction, precedence);

		IEnumerable<int> indices = Enumerable.Range(0, tokens.Length);
		if (direction == EvaluationDirection.LeftToRight)
			indices = indices.Reverse();

		foreach (int i in indices)
		{
			if (tokens[i] is AddToken && IsAddSplitAllowed(tokens, precedence))
				return Evaluate(tokens[..i], direction, precedence) + Evaluate(tokens[(i + 1)..], direction, precedence);
			else if (tokens[i] is MultiplyToken && IsMultiplySplitAllowed(tokens, precedence))
				return Evaluate(tokens[..i], direction, precedence) * Evaluate(tokens[(i + 1)..], direction, precedence);
		}
		throw new Exception($"Invalid Token list: {tokens}");
	}

	public static bool IsAddSplitAllowed(IToken[] tokens, OperatorPrecedence precedence)
	{
		return precedence == OperatorPrecedence.PointBeforeLine || precedence == OperatorPrecedence.None || !tokens.Any((t) => t is MultiplyToken);
	}

	public static bool IsMultiplySplitAllowed(IToken[] tokens, OperatorPrecedence precedence)
	{
		return precedence == OperatorPrecedence.LineBeforePoint || precedence == OperatorPrecedence.None || !tokens.Any((t) => t is AddToken);
	}
}

public enum EvaluationDirection
{
	LeftToRight,
	RightToLeft
}

public enum OperatorPrecedence
{
	PointBeforeLine,
	LineBeforePoint,
	None
}