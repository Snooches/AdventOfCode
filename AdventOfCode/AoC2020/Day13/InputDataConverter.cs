using Utilities.Interfaces;

namespace AoC2020.Day13;

public class InputDataConverter : IInputDataConverter<(int, IEnumerable<int>)>
{
	public (int, IEnumerable<int>) ConvertInputData(IFileReader fileReader)
	{
		List<string> input = fileReader.ReadLines().ToList();
		if (input.Count < 2)
			return (0, new List<int>());
		if (!Int32.TryParse(input[0], out int time))
			return (0, new List<int>());
		return (time, GetBusIds(input[1]));
	}

	private IEnumerable<int> GetBusIds(string inputLine)
	{
		foreach (string id in inputLine.Split(','))
			if (Int32.TryParse(id, out int result))
				yield return result;
			else if (id == "x")
				yield return 0;
	}
}