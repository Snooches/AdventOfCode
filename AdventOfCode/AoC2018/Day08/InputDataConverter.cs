using Utilities.Interfaces;

namespace AoC2018.Day08;

internal class InputDataConverter : IInputDataConverter<TreeNode>
{
	public TreeNode ConvertInputData(IFileReader fileReader)
	{
		List<int> input = fileReader.ReadLines().First().Split(' ').Select(int.Parse).ToList();
		int currentIndex = 0;
		return ReadNode(input, ref currentIndex);
	}

	private static TreeNode ReadNode(List<int> input, ref int currentIndex)
	{
		TreeNode node = new();
		int childCount = input[currentIndex];
		int dataCount = input[currentIndex + 1];
		currentIndex += 2;
		for (int i = 0; i < childCount; i++)
		{
			node.Children.Add(ReadNode(input, ref currentIndex));
		}
		node.Payload = input[currentIndex..(currentIndex + dataCount)];
		currentIndex += dataCount;
		return node;
	}
}