namespace Utilities.GameOfLife;

public class GameOfLifeController<CellType, MetaDataType>
	where CellType : AbstractGameOfLifeCell<MetaDataType>, new()
{
	public int LivingCellCount => CellList.Count((cell) => cell.IsAlive);
	public bool IsStabilized { get; private set; }

	private readonly Dictionary<string, CellType> Cells;
	private List<CellType> CellList => Cells.Values.ToList();
	private readonly bool expandingBoard;

	public GameOfLifeController(bool expanding = false)
	{
		expandingBoard = expanding;
		Cells = new();
	}

	public void Step()
	{
		if (expandingBoard)
			AdjustBoard();
		CellList.ForEach((cell) => cell.CalculateNextState());
		if (CellList.Count == 0)
			IsStabilized = true;
		else
			IsStabilized = !CellList.Select((cell) => cell.AssumeNextState()).Aggregate((b1, b2) => b1 || b2);
	}

	private void AdjustBoard()
	{
		List<string> CellsToAdd = new();
		List<string> CellsToDelete = new();
		foreach (CellType cell in Cells.Values)
		{
			foreach (string key in cell.NeighbouringKeys)
			{
				if (cell.IsAlive && !Cells.ContainsKey(key) && !CellsToAdd.Contains(key))
					CellsToAdd.Add(key);
			}
		}
		if (CellsToAdd.Count == 0)
			return;
		CellType template = Cells.Values.First();
		foreach (string key in CellsToAdd)
			AddCell(new CellType() { Key = key, ReviveRange = template.ReviveRange, KeepAliveRange = template.KeepAliveRange, MetaData = template.MetaData });
		foreach (string key in CellsToDelete)
			RemoveCell(Cells[key]);
	}

	public void AddCell(CellType newCell)
	{
		foreach (string neighbourKey in newCell.NeighbouringKeys)
		{
			if (Cells.ContainsKey(neighbourKey))
			{
				Cells[neighbourKey].AddNeighbour(newCell);
				newCell.AddNeighbour(Cells[neighbourKey]);
			}
		}
		Cells.Add(newCell.Key, newCell);
	}

	public void RemoveCell(string key) => RemoveCell(Cells[key]);

	public void RemoveCell(CellType cell)
	{
		foreach (string neighbourKey in cell.NeighbouringKeys)
		{
			if (Cells.ContainsKey(neighbourKey))
			{
				Cells[neighbourKey].RemoveNeighbour(cell);
			}
		}
		Cells.Remove(cell.Key);
	}
}