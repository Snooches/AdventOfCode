using System.Text;
using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day23;

public class Solver : AbstractSolver<IEnumerable<int>, string, long>
{
	protected override string SolutionTextA => $"{SolutionValueA}";
	protected override string SolutionTextB => $"{SolutionValueB}";

	public Solver(IInputDataConverter<IEnumerable<int>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
		if (inputData.Count() < 5)
			throw new Exception("input to small");
	}

	private int[] cups = new int[1];
	private int currentCup;

	protected override void SolveImplemented()
	{
		BuildCupsArray();
		for (int i = 0; i < 100; i++)
			MakeMove2();
		SolutionValueA = GetResultA2();
		BuildCupsArray(1000000);
		for (int i = 0; i < 10000000; i++)
			MakeMove2();
		SolutionValueB = GetResultB2();
	}

	private void BuildCupsArray()
	{
		cups = new int[inputData.Max() + 1];
		currentCup = inputData.First();
		int previousCup = 0;
		foreach (int cup in inputData)
		{
			if (previousCup > 0)
				cups[previousCup] = cup;
			previousCup = cup;
		}
		cups[previousCup] = currentCup;
	}

	private void BuildCupsArray(long length)
	{
		cups = new int[length + 1];
		currentCup = inputData.First();
		int previousCup = 0;
		foreach (int cup in inputData)
		{
			if (previousCup > 0)
				cups[previousCup] = cup;
			previousCup = cup;
		}
		for (int cup = inputData.Max() + 1; cup <= length; cup++)
		{
			cups[previousCup] = cup;
			previousCup = cup;
		}
		cups[previousCup] = currentCup;
	}

	private void MakeMove2()
	{
		int target = currentCup;
		while (target == currentCup ||
			   target == cups[currentCup] ||
			   target == cups[cups[currentCup]] ||
			   target == cups[cups[cups[currentCup]]])
		{
			target--;
			if (target < 1)
				target += (cups.Length - 1);
		}
		int lastItem = cups[cups[cups[currentCup]]];
		int temp = cups[currentCup];
		cups[currentCup] = cups[lastItem];
		cups[lastItem] = cups[target];
		cups[target] = temp;

		currentCup = cups[currentCup];
	}

	private string GetResultA2()
	{
		StringBuilder str = new();
		int cup = cups[1];
		while (true)
		{
			if (cup == 1)
				return str.ToString();
			str.Append(cup);
			cup = cups[cup];
		}
	}

	private long GetResultB2()
	{
		return (long)cups[1] * cups[cups[1]];
	}
}