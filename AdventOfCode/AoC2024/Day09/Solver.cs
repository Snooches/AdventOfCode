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
		Task.Delay(1000); //Just as a reminder that this solution needs to be cleanedUp/reworked
		IEnumerable<int> compactList = CompactWithFragmentation(inputData);
		SolutionValueA = Checksum(compactList);

		compactList = CompactWithoutFragmentation(inputData);
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
		List<int> input = fileList.ToList();
		List<MemoryBlock> blockList = [];
		for (int i = 0; i < input.Count; i++)
			if (i % 2 == 0)
				blockList.Add(new(i / 2, input[i], false));
			else
				blockList.Add(new(0, input[i], true));
		int blockIndex = blockList.Count - 1;
		while (blockIndex >= 0)
		{
			bool blockMoved = false;
			int spaceIndex = 1;
			while(spaceIndex < blockIndex)
			{
				if (blockList[spaceIndex].Length >= blockList[blockIndex].Length)
				{
					blockList[spaceIndex] = new(0, blockList[spaceIndex].Length - blockList[blockIndex].Length, true);
					blockList.Insert(spaceIndex, blockList[blockIndex]);
					blockList.Insert(spaceIndex, new(0, 0, true));
					blockList[blockIndex + 2] = new(0, blockList[blockIndex+2].Length,true);
					blockMoved = true;
					break;
				}
				spaceIndex += 2;
			}
			if (!blockMoved)
				blockIndex -= 2;
		}
		foreach(MemoryBlock block in blockList)
		{
			for (int i = 0; i < block.Length; i++)
			{
				yield return block.IsFree ? 0 : block.Id;
			}
		}
	}

	private sealed record MemoryBlock(int Id, int Length, bool IsFree) { }
}