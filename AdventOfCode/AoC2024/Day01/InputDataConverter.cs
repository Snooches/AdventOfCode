using Utilities.Interfaces;

namespace AoC2024.Day01;

internal class InputDataConverter : IInputDataConverter<(List<int>, List<int>)>
{
	public (List<int>, List<int>) ConvertInputData(IFileReader fileReader)
	{
		List<int> listA = [];
		List<int> listB = [];

		foreach (string line in fileReader.ReadLines())
		{
			String[] split = line.Split("   ");
			listA.Add(int.Parse(split[0]));
			listB.Add(int.Parse(split[1]));
		}

		return (listA, listB);
	}
}