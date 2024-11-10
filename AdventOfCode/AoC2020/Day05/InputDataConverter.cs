using System.Text.RegularExpressions;
using Utilities.Interfaces;

namespace AoC2020.Day05;

public class InputDataConverter : IInputDataConverter<IEnumerable<Seat>>
{
	private Regex formatVerifier = new("[F|B]{7}[L|R]{3}$");

	public IEnumerable<Seat> ConvertInputData(IFileReader fileReader)
	{
		foreach (string line in fileReader.ReadLines())
		{
			if (formatVerifier.IsMatch(line))
			{
				string id = line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');
				yield return new Seat() { Id = Convert.ToInt32(id, 2) };
			}
		}
	}
}