namespace Utilities.GameOfLife;

public interface IGameOfLifeCell
{
	public string Key { get; init; }
	public IEnumerable<IGameOfLifeCell> Neighbours { get; }
	public IEnumerable<string> NeighbouringKeys { get; }
	public bool IsAlive { get; set; }

	public void CalculateNextState();

	public bool AssumeNextState();

	public void AddNeighbour(IGameOfLifeCell neighbour);
}