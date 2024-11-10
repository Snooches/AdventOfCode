using Utilities;
using Utilities.Interfaces;

namespace AoC2023.Day08;

internal class Solver(IInputDataConverter<(char[], Dictionary<string, (string, string)>)> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<(char[] Instructions, Dictionary<string, (string Left, string Right)> Nodes), long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"The reuired number of steps to reach 'ZZZ' is: {SolutionValueA}";
	protected override string SolutionTextB => $"The required number of steps to reach all nodes ending in 'Z' is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		string currentNode = "AAA";
		int currentInstruction = 0;
		SolutionValueA = 0;
		while (currentNode != "ZZZ")
		{
			SolutionValueA++;
			if (inputData.Instructions[currentInstruction] == 'L')
				currentNode = inputData.Nodes[currentNode].Left;
			else
				currentNode = inputData.Nodes[currentNode].Right;
			currentInstruction++;
			if (currentInstruction >= inputData.Instructions.Length)
				currentInstruction = 0;
		}

		(string, List<int>)[] currentNodes = inputData.Nodes.Keys.Where(x => x.EndsWith('A')).Select(x => (x, new List<int>())).ToArray();
		currentInstruction = 0;
		int instructionCycle = 0;
		SolutionValueB = 0;
		while (currentNodes.Any(x => x.Item2.Count < 2))
		{
			SolutionValueB++;
			if (inputData.Instructions[currentInstruction] == 'L')
				for (int nodeId = 0; nodeId < currentNodes.Length; nodeId++)
				{
					currentNodes[nodeId].Item1 = inputData.Nodes[currentNodes[nodeId].Item1].Left;
					if (currentNodes[nodeId].Item1.EndsWith('Z'))
						currentNodes[nodeId].Item2.Add(instructionCycle * inputData.Instructions.Length + currentInstruction);
				}
			else
				for (int nodeId = 0; nodeId < currentNodes.Length; nodeId++)
				{
					currentNodes[nodeId].Item1 = inputData.Nodes[currentNodes[nodeId].Item1].Right;
					if (currentNodes[nodeId].Item1.EndsWith('Z'))
						currentNodes[nodeId].Item2.Add(instructionCycle * inputData.Instructions.Length + currentInstruction);
				}
			currentInstruction++;
			if (currentInstruction >= inputData.Instructions.Length)
			{
				currentInstruction = 0;
				instructionCycle++;
			}
		}
		SolutionValueB = MathUtils.LCM(currentNodes.Select(x => (long)(x.Item2[1] - x.Item2[0])));
	}
}