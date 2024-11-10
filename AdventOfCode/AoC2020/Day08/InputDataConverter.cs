using Utilities.Interfaces;

namespace AoC2020.Day08;

public class InputDataConverter : IInputDataConverter<IEnumerable<(BootCoderCommand, int)>>
{
	public IEnumerable<(BootCoderCommand, int)> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			var split = line.Split(' ');
			if (split.Length != 2)
				continue;
			if (Int32.TryParse(split[1], out int parameter))
				switch (split[0])
				{
					case "nop":
						yield return (BootCoderCommand.NoOperation, parameter);
						break;

					case "jmp":
						yield return (BootCoderCommand.Jump, parameter);
						break;

					case "acc":
						yield return (BootCoderCommand.Accumulate, parameter);
						break;
				}
		}
	}
}