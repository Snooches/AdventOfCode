using System.Diagnostics;
using System.Text;
using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day22;

public class Solver : AbstractSolver<(IEnumerable<byte>, IEnumerable<byte>), long, long>
{
	protected override string SolutionTextA => $"{SolutionValueA}";
	protected override string SolutionTextB => $"{SolutionValueB}";

	public Solver(IInputDataConverter<(IEnumerable<byte>, IEnumerable<byte>)> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader)
	{
		numberOfCards = inputData.Item1.Count() + inputData.Item2.Count();
		cardIDLength = (byte)Math.Ceiling(Math.Log10(numberOfCards));
	}

	private readonly int numberOfCards;
	private readonly byte cardIDLength;

	protected override void SolveImplemented()
	{
		Stopwatch watch = new();
		Queue<byte> Player1Deck = new(inputData.Item1);
		Queue<byte> Player2Deck = new(inputData.Item2);

		watch.Start();
		PlayGame(Player1Deck, Player2Deck);
		watch.Stop();
		Console.WriteLine($"Non Recursive play: {watch.Elapsed}");

		var winner = Player1Deck.Count > 0 ? Player1Deck : Player2Deck;
		SolutionValueA = ScoreDeck(winner);

		Player1Deck = new(inputData.Item1);
		Player2Deck = new(inputData.Item2);

		watch.Restart();
		PlayGame(Player1Deck, Player2Deck, true);
		watch.Stop();
		Console.WriteLine($"Recursive play: {watch.Elapsed}");

		winner = Player1Deck.Count > 0 ? Player1Deck : Player2Deck;
		SolutionValueB = ScoreDeck(winner);
	}

	private void PlayGame(Queue<byte> Player1Deck, Queue<byte> Player2Deck, bool recursive = false)
	{
		HashSet<string> gameStates = new();
		while (Player1Deck.Count > 0 && Player2Deck.Count > 0)
		{
			var state = GameState(Player1Deck, Player2Deck);
			if (gameStates.Contains(state))
			{
				Player2Deck.Clear();
				return;
			}
			else
			{
				gameStates.Add(state);
			}
			if (recursive && Player1Deck.Count > Player1Deck.Peek() && Player2Deck.Count > Player2Deck.Peek())
			{
				Queue<byte> player1SubDeck = new(Player1Deck.ToArray()[1..(Player1Deck.Peek() + 1)]);
				Queue<byte> player2SubDeck = new(Player2Deck.ToArray()[1..(Player2Deck.Peek() + 1)]);
				PlayGame(player1SubDeck, player2SubDeck, true);
				if (player1SubDeck.Count > 0)
				{
					Player1Deck.Enqueue(Player1Deck.Dequeue());
					Player1Deck.Enqueue(Player2Deck.Dequeue());
				}
				else
				{
					Player2Deck.Enqueue(Player2Deck.Dequeue());
					Player2Deck.Enqueue(Player1Deck.Dequeue());
				}
			}
			else if (Player1Deck.Peek() > Player2Deck.Peek())
			{
				Player1Deck.Enqueue(Player1Deck.Dequeue());
				Player1Deck.Enqueue(Player2Deck.Dequeue());
			}
			else if (Player2Deck.Peek() > Player1Deck.Peek())
			{
				Player2Deck.Enqueue(Player2Deck.Dequeue());
				Player2Deck.Enqueue(Player1Deck.Dequeue());
			}
			else
			{
				//Should never happen unless there are duplicate cards
				throw new Exception("Draw!");
			}
		}
	}

	private string GameState(Queue<byte> deck1, Queue<byte> deck2)
	{
		StringBuilder str = new(numberOfCards * (cardIDLength + 1) + 1);
		foreach (int item in deck1)
			str.Append(item);
		str.Append('|');
		foreach (int item in deck2)
			str.Append(item);
		return str.ToString();
	}

	private static long ScoreDeck(Queue<byte> deck)
	{
		Queue<byte> copy = new(deck);
		long score = 0;
		while (copy.Count > 0)
		{
			score += copy.Count * copy.Dequeue();
		}
		return score;
	}
}