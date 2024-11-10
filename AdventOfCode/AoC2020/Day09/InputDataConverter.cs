using Utilities.Interfaces;

namespace AoC2020.Day09
{
	public class InputDataConverter : IInputDataConverter<IEnumerable<long>>
	{
		public IEnumerable<long> ConvertInputData(IFileReader fileReader)
		{
			foreach (string line in fileReader.ReadLines())
				if (Int64.TryParse(line, out long result))
					yield return result;
		}
	}
}