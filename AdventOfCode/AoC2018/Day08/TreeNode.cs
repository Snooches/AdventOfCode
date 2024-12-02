namespace AoC2018.Day08;

internal class TreeNode
{
	public List<int> Payload { get; set; } = [];
	public List<TreeNode> Children { get; set; } = [];

	public int SumOfPayloads()
	{
		return Payload.Sum() + Children.Sum(x => x.SumOfPayloads());
	}

	public int NodeValue() 
	{
		if (Children.Count == 0)
		{
			return Payload.Sum();
		}
		int value = 0;
		foreach(int index in Payload)
		{
			if(index > 0 && index <= Children.Count)
			{
				value += Children[index - 1].NodeValue();
			}
		}
		return value;
	}
}