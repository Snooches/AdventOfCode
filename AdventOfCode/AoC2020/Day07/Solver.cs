using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day07;

public class Solver : AbstractSolver<IEnumerable<DirectedWeightedGraphNode<string, string>>, int, int?>
{
	protected override string SolutionTextA => $"{SolutionValueA} different bag types may eventually contain a {TargetBag} bag.";

	protected override string SolutionTextB => $"My {TargetBag} bag has to contain {SolutionValueB} other bags.";

	public string TargetBag { get; set; } = "shiny gold";

	public Solver(IInputDataConverter<IEnumerable<DirectedWeightedGraphNode<string, string>>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		SolutionValueA = 0;
		DirectedWeightedGraphNode<string, string>? targetNode = inputData.Where(t => t.Key == TargetBag).FirstOrDefault();
		if (targetNode is not null)
		{
			SolutionValueA += targetNode.GetAncestors().Select((node) => node.Key).Distinct().Count() - 1; //Don't count the bag itself

			SolutionValueB = 0;
			foreach ((DirectedWeightedGraphNode<string, string> node, double quantifier) in targetNode.Children.Values)
			{
				SolutionValueB += (int)(GetTotalBagCount(node) * quantifier);
			}
		}
	}

	private double GetTotalBagCount(DirectedWeightedGraphNode<string, string> node)
	{
		double totalBagCount = 1;
		foreach ((DirectedWeightedGraphNode<string, string> child, double quantifier) in node.Children.Values)
		{
			totalBagCount += GetTotalBagCount(child) * quantifier;
		}
		return totalBagCount;
	}
}