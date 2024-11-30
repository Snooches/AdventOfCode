using System.Text;
using Utilities;
using Utilities.Interfaces;

namespace AoC2018.Day07;

internal class Solver(IInputDataConverter<Dictionary<char, ProcessStep>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<Dictionary<char, ProcessStep>, string?, int?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"SolutionA is: {SolutionValueA}";
	protected override string SolutionTextB => $"SolutionB is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		StringBuilder solutionCollector = new();
		HashSet<ProcessStep> processedSteps = [];
		SortedSet<char> possibleSteps = new(inputData.Values.Where(x => x.Predecessors.Count == 0).Select(x => x.Id));

		while (possibleSteps.Count > 0)
		{
			ProcessStep nextStep = inputData[possibleSteps.First()];
			solutionCollector.Append(nextStep.Id);
			processedSteps.Add(nextStep);
			possibleSteps.Remove(nextStep.Id);
			foreach (ProcessStep step in nextStep.Successors)
			{
				if (!step.Predecessors.Except(processedSteps).Any())
					possibleSteps.Add(step.Id);
			}
		}
		SolutionValueA = solutionCollector.ToString();

		int timer = 0;
		SortedList<int, HashSet<char>> eventQueue = [];
		processedSteps = [];
		possibleSteps = new(inputData.Values.Where(x => x.Predecessors.Count == 0).Select(x => x.Id));

		while(processedSteps.Count < inputData.Count)
		{
			while (eventQueue.Count < 5 && possibleSteps.Count > 0)
			{
				ProcessStep nextStep = inputData[possibleSteps.First()];
				int completionTime = timer + (nextStep.Id - 4);
				if (eventQueue.TryGetValue(completionTime, out HashSet<char>? events))
					events.Add(nextStep.Id);
				else
					eventQueue.Add(completionTime, [nextStep.Id]);
				possibleSteps.Remove(nextStep.Id);
			}

			KeyValuePair<int,HashSet<char>> finishedSteps = eventQueue.First();
			timer = finishedSteps.Key;
			foreach (char stepId in finishedSteps.Value)
			{
				ProcessStep processedStep = inputData[stepId];
				processedSteps.Add(processedStep);
				foreach(ProcessStep successor in processedStep.Successors)
				{
					if (!successor.Predecessors.Except(processedSteps).Any())
					{
						possibleSteps.Add(successor.Id);
					}
				}
			}
			eventQueue.Remove(finishedSteps.Key);
		}

		SolutionValueB = timer;
	}
}