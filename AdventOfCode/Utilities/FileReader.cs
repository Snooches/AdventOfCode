using Utilities.Interfaces;

namespace Utilities;

public class FileReader(string path) : IFileReader
{
	public IEnumerable<string> ReadLines()
	{
		return File.ReadLines(path);
	}
}