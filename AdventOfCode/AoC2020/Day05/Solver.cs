using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day05;

public class Solver : AbstractSolver<IEnumerable<Seat>, Seat, Seat>
{
	protected override string SolutionTextA => $"The seat with the Highest ID is {SolutionValueA.Id} (Row:{SolutionValueA.Row}, Seat:{SolutionValueA.Column}).";
	protected override string SolutionTextB => $"My seat has the ID {SolutionValueB.Id} (Row:{SolutionValueB.Row}, Seat:{SolutionValueB.Column}).";

	public Solver(IInputDataConverter<IEnumerable<Seat>> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
	}

	protected override void SolveImplemented()
	{
		if (inputData.Any())
			return;
		SolutionValueA = new Seat() { Id = inputData.Max((seat) => seat.Id) };

		Seat? precedingSeat = inputData.Where((seat) => !inputData.Any((skippedSeat) => skippedSeat.Id == seat.Id + 1) &&
																									 inputData.Any((nextSeat) => nextSeat.Id == seat.Id + 2)).FirstOrDefault();
		if (precedingSeat is not null)
			SolutionValueB = new Seat() { Id = precedingSeat.Id + 1 };
	}
}