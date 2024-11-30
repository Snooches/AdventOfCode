using Utilities.Interfaces;

namespace AoC2018.Day07;

internal class InputDataConverter : IInputDataConverter<Dictionary<char,ProcessStep>>
{
	public Dictionary<char, ProcessStep> ConvertInputData(IFileReader fileReader)
	{
		Dictionary<char, ProcessStep> steps = [];
		foreach (string line in fileReader.ReadLines())
		{
			char predecessorId = line[5];
			char successorId = line[36];
			if(!steps.TryGetValue(predecessorId, out ProcessStep predecessor))
			{
				predecessor = new ProcessStep(predecessorId);
				steps.Add(predecessorId, predecessor);
			}
			if(!steps.TryGetValue(successorId, out ProcessStep successor))
			{
				successor = new ProcessStep(successorId);
				steps.Add(successorId , successor);
			}
			predecessor.Successors.Add(successor);
			successor.Predecessors.Add(predecessor);
		}
		return steps;
	}
}