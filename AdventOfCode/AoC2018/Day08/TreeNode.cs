namespace AoC2018.Day08;

internal class TreeNode
{
	private HashSet<int> Payload { get; set; }
	private HashSet<TreeNode> Children { get; set; }
}