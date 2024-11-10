using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Day18.Tokens
{
	public class SubListToken: IToken
	{
		public override string ToString()
		{
			var result = new StringBuilder();
			result.Append('(');
			foreach(IToken t in Tokens)
				result.Append(t.ToString());
			result.Append(')');
			return result.ToString();
		}

		public long Evaluate(EvaluationDirection direction, OperatorPrecedence precedence)
		{
			return TermEvaluator.Evaluate(Tokens, direction, precedence);
		}

		public IToken[] Tokens { get; }
		public SubListToken(string input)
		{
			Tokens = Tokenizer.Tokenize(input).ToArray();
		}
	}
}
