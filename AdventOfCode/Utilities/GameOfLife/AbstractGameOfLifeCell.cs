using System.Collections.Immutable;

namespace Utilities.GameOfLife;

public abstract class AbstractGameOfLifeCell<MetaDataType> : IGameOfLifeCell
{
	public virtual string Key { get; init; } = String.Empty;
	protected List<IGameOfLifeCell> neighbours = new();
	public IEnumerable<IGameOfLifeCell> Neighbours => neighbours;
	public abstract IEnumerable<string> NeighbouringKeys { get; }
	public bool IsAlive { get; set; }
	private bool? NextState;
	public int[] ReviveRange { get; init; } = Array.Empty<int>();
	public int[] KeepAliveRange { get; init; } = Array.Empty<int>();
	public abstract MetaDataType MetaData { get; init; }

	public void AddNeighbour(IGameOfLifeCell neighbour)
	{
		if (!neighbours.Any((cell) => cell.Key == Key))
			neighbours.Add(neighbour);
	}

	public void RemoveNeighbour(IGameOfLifeCell neighbour)
	{
		neighbours.RemoveAll((cell) => cell.Key == neighbour.Key);
	}

	public void CalculateNextState()
	{
		int livingNeighbours = Neighbours.Count((cell) => cell.IsAlive);
		if (!IsAlive && ReviveRange.Contains(livingNeighbours))
			NextState = true;
		else if (IsAlive && !KeepAliveRange.Contains(livingNeighbours))
			NextState = false;
		else
			NextState = IsAlive;
	}

	public bool AssumeNextState()
	{
		if (NextState is null)
		{
			CalculateNextState();
		}
		bool result = IsAlive != NextState;
		IsAlive = (bool)NextState!;
		NextState = null;
		return result;
	}
}