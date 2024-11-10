using Utilities.Interfaces;

namespace AoC2020.Day14;

public class InputDataConverter : IInputDataConverter<IEnumerable<(BitMask, long, long)>>
{
	public IEnumerable<(BitMask, long, long)> ConvertInputData(IFileReader fileReader)
	{
		BitMask? currentMask = null;
		foreach (string line in fileReader.ReadLines())
		{
			string[] split = line.Split(" = ");
			if (split.Length != 2)
				continue;
			if (split[0] == "mask")
			{
				if (split[1].Any(c => !"01X".Contains(c)) || split[1].Length != 36)
					throw new Exception("Invalid Mask");
				currentMask = new() { MaskString = split[1] };
			}
			else if (split[0].StartsWith("mem"))
			{
				if (currentMask is null)
					throw new Exception("No Mask Set");
				if (Int64.TryParse(split[0][4..^1], out long adr) && Int64.TryParse(split[1], out long value))
					yield return (currentMask, adr, value);
			}
		}
	}
}