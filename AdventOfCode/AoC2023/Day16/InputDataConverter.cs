using Utilities.Interfaces;

namespace AoC2023.Day16;

internal class InputDataConverter : IInputDataConverter<MirrorTile[,]>
{
	public MirrorTile[,] ConvertInputData(IFileReader fileReader)
	{
		IList<string> inputLines = fileReader.ReadLines().ToList();
		MirrorTile[,] result = new MirrorTile[inputLines[0].Length, inputLines.Count];
		for (int y = 0; y < inputLines.Count; y++)
			for (int x = 0; x < inputLines[y].Length; x++)
				result[x, y] = new MirrorTile(inputLines[y][x] switch
				{
					'.' => MirrorTileType.Empty,
					'|' => MirrorTileType.SplitterVertical,
					'-' => MirrorTileType.SplitterHorizontal,
					'/' => MirrorTileType.MirrorTopRight,
					'\\' => MirrorTileType.MirrorTopLeft,
					_ => MirrorTileType.Empty
				});
		return result;
	}
}