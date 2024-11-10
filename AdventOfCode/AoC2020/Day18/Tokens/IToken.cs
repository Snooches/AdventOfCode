using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2020.Day18.Tokens
{
	public interface IToken
	{
		public long Evaluate(EvaluationDirection direction, OperatorPrecedence precedence);
	}
}
