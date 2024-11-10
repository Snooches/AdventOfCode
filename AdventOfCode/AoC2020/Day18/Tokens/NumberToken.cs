using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Day18.Tokens
{
	public class NumberToken: IToken
	{
		public long Value { get; }
		public NumberToken(string input)
		{
			this.Value = Int64.Parse(input);
		}
		public override string ToString() => Value.ToString();

		public long Evaluate(EvaluationDirection direction, OperatorPrecedence precedence)
		{
			return Value;
		}
	}
}
