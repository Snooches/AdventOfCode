using System.Text;

namespace AoC2020.Day14;

public class BitMask
{
	public string? MaskString { get; init; } = null;

	public string ApplyMask(string source)
	{
		if (MaskString is null)
			throw new NullReferenceException("MaskString is not set");
		StringBuilder result = new();
		string paddedSource = source.PadLeft(MaskString.Length, '0');
		for (int i = 0; i < paddedSource.Length; i++)
		{
			if (i >= MaskString.Length || MaskString[i] == 'X')
				result.Append(paddedSource[i]);
			else
				result.Append(MaskString[i]);
		}
		return result.ToString();
	}

	public IEnumerable<string> ApplyMaskWithFloatingBits(string source)
	{
		if (MaskString is null)
			throw new NullReferenceException("MaskString is not set");
		string paddedSource = source.PadLeft(MaskString.Length, '0');
		List<StringBuilder> result = new() { new(MaskString.Length) };
		for (int i = 0; i < MaskString.Length; i++)
		{
			switch (MaskString[i])
			{
				case '0':
					result.ForEach((s) => s.Append(paddedSource[i]));
					break;

				case '1':
					result.ForEach((s) => s.Append('1'));
					break;

				case 'X':
					List<StringBuilder> copy = result.Select((sb) => new StringBuilder(sb.ToString())).ToList();
					result.ForEach((s) => s.Append('0'));
					copy.ForEach((s) => s.Append('1'));
					result.AddRange(copy);
					break;
			}
		}
		return result.Select((s) => s.ToString());
	}
}