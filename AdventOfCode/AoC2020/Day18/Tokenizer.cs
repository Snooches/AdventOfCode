using AoC2020.Day18.Tokens;

namespace AoC2020.Day18;

public class Tokenizer
{
	public static IEnumerable<IToken> Tokenize(string input)
	{
		int start = 0;
		int end = 0;
		while (start < input.Length)
		{
			switch (input[start])
			{
				case '+':
					start++;
					yield return new AddToken();
					break;

				case '*':
					start++;
					yield return new MultiplyToken();
					break;

				case '(':
					int openParantheses = 0;
					for (int i = start; i < input.Length; i++)
					{
						if (input[i] == '(')
							openParantheses++;
						else if (input[i] == ')')
							openParantheses--;
						if (openParantheses == 0)
						{
							end = i + 1;
							break;
						}
					}
					yield return new SubListToken(input[(start + 1)..(end - 1)]);
					start = end;
					break;

				case char c when Char.IsDigit(c):
					end = start;
					while (end < input.Length && char.IsDigit(input[end]))
						end++;
					yield return new NumberToken(input[start..end]);
					start = end;
					break;
			}
		}
	}
}