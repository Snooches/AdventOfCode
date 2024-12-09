using Utilities;
using Utilities.Interfaces;

namespace AoC2024.Day09;

internal class Solver(IInputDataConverter<IEnumerable<int>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<int>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		Console.WriteLine(String.Join("", inputData.Select(x=>$"{x}")));
		IEnumerable<int> compactList = CompactWithFragmentation(inputData);
		Console.WriteLine(String.Join("", compactList.Select(x => $"{x}")));
		SolutionValueA = Checksum(compactList);

		compactList = CompactWithoutFragmentation(inputData);
		Console.WriteLine(String.Join("", compactList.Select(x => $"{x}")));
		SolutionValueB = Checksum(compactList);
	}

	private static long Checksum(IEnumerable<int> fileList)
	{
		long checksum = 0;
		int index = 0;
		foreach (int fileId in fileList)
		{
			checksum += fileId * index;
			index++;
		}
		return checksum;
	}

	private static IEnumerable<int> CompactWithFragmentation(IEnumerable<int> fileList)
	{
		List<int> inputList = fileList.ToList();
		int indexFront = 0;
		int indexBack = inputList.Count - 1;
		int fileChuncksLeft = inputList[indexBack];
		while(indexFront < indexBack)
		{
			if (indexFront % 2 == 0)
				for (int i = 0; i < inputList[indexFront];i++)
					yield return (int)(indexFront/(decimal)2);
			else
			{
				for (int i = 0; i < inputList[indexFront]; i++)
				{
					if (fileChuncksLeft == 0)
					{
						indexBack -= 2;
						fileChuncksLeft = inputList[indexBack];
					}
					yield return (int)(indexBack / (decimal)2);
					fileChuncksLeft--;
				}
			}
			indexFront++;
		}
		for (int i = 0; i < fileChuncksLeft; i++)
			yield return (int)(indexFront / (decimal)2);
	}

	private static IEnumerable<int> CompactWithoutFragmentation(IEnumerable<int> fileList)
	{
		List<int> inputList = fileList.ToList();
		int indexFront = 0;
		int indexBack = inputList.Count - 1;
	}
}