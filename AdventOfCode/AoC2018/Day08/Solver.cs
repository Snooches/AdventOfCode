using Utilities;
using Utilities.Interfaces;

namespace AoC2018.Day08;

internal class Solver(IInputDataConverter<TreeNode> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<TreeNode, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		SolutionValueA = inputData.SumOfPayloads();
		SolutionValueB = inputData.NodeValue();
	}
}