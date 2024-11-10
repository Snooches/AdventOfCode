using System.Diagnostics;
using System.Reflection;
using Utilities;
using Utilities.Interfaces;

const string AoCYear = "2023";

string input = String.Empty;
while (input != "exit")
{
	Console.Write("Which puzzle should be solved? ");
	input = Console.ReadLine() ?? "";
	IEnumerable<ISolver> solvers;
	if (Int32.TryParse(input, out int selectedDay) && selectedDay >= 1 && selectedDay <= 25)
	{
		solvers = GetSolver(new List<int> { selectedDay });
	}
	else
	{
		switch (input)
		{
			case "all":
				solvers = GetSolver(Enumerable.Range(1, 25).ToList());
				break;

			case "exit":
				continue;

			default:
				Console.WriteLine("Invalid command");
				Console.WriteLine();
				continue;
		}
	}
	Stopwatch timer = new();
	timer.Start();
	foreach (ISolver solver in solvers)
	{
		Stopwatch timer2 = new();
		timer2.Start();
		solver.Solve();
		timer2.Stop();
		Console.WriteLine($"Solution for {solver.GetType().Namespace}:");
		Console.WriteLine($"Part 1: {solver.SolutionA}");
		Console.WriteLine($"Part 2: {solver.SolutionB}");
		Console.WriteLine($"Solver ran for {timer2.Elapsed:hh\\:mm\\:ss\\.fff}");
	}
	timer.Stop();
	Console.WriteLine();
	Console.WriteLine($"Solvers ran for {timer.Elapsed:hh\\:mm\\:ss\\.fff} total.");
	Console.WriteLine();
}

static IEnumerable<ISolver> GetSolver(IEnumerable<int> input)
{
	List<ISolver> solvers = [];
	foreach (int day in input)
	{
		Type? converterType = Type.GetType($"AoC{AoCYear}.Day{day:D2}.InputDataConverter");
		ConstructorInfo? converterConstructor = converterType?.GetConstructor([]);
		Object? inputConverter = converterConstructor?.Invoke(null);
		if (inputConverter is not IInputDataConverter typedConverter)
		{
			Console.WriteLine($"Failed to create InputConverter for Day {day}. It probably has not been implemented yet.");
			continue;
		}

		Type? solverType = Type.GetType($"AoC{AoCYear}.Day{day:D2}.Solver");
		ConstructorInfo? solverConstructor = solverType?.GetConstructor([converterType!, typeof(IFileReader)]);
		Object? solver = solverConstructor?.Invoke(new object[] { typedConverter, new FileReader($"Day{day:D2}/InputData.txt") });
		if (solver is ISolver typedSolver)
		{
			solvers.Add(typedSolver);
		}
		else
		{
			Console.WriteLine($"Failed to create Solver for Day {day}. It probably has not been implemented yet.");
		}
	}
	return solvers;
}