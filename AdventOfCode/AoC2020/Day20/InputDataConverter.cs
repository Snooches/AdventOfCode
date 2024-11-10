using Utilities.Interfaces;

namespace AoC2020.Day20;

public class InputDataConverter : IInputDataConverter<IEnumerable<ImageTile>>
{
	public IEnumerable<ImageTile> ConvertInputData(IFileReader fileReader)
	{
		int id = 0;
		List<string> grid = new();

		foreach (string line in fileReader.ReadLines())
		{
			if (line.StartsWith("Tile"))
			{
				id = Int32.Parse(line[5..^1]);
				grid = new();
				continue;
			}
			if (line.Length > 0)
			{
				grid.Add(line);
				continue;
			}
			yield return new ImageTile(grid) { ID = id };
		}
		if (grid.Count > 0)
		{
			yield return new ImageTile(grid) { ID = id };
		}
	}
}