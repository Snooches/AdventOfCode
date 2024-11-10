using System.Text;
using Utilities.Interfaces;

namespace AoC2023.Day06;

internal class InputDataConverter : IInputDataConverter<(IEnumerable<Race>, Race)>
{
	public (IEnumerable<Race>, Race) ConvertInputData(IFileReader fileReader)
	{
		List<string> input = fileReader.ReadLines().ToList();
		string[] times = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..];
		string[] distances = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..];
		IList<Race> shortRaces = [];
		StringBuilder longRaceTime = new();
		StringBuilder longRaceDistance = new();
		for (int i = 0; i < times.Length; i++)
		{
			shortRaces.Add(new Race(long.Parse(times[i]), long.Parse(distances[i])));
			longRaceTime.Append(times[i]);
			longRaceDistance.Append(distances[i]);
		}
		return (shortRaces, new Race(long.Parse(longRaceTime.ToString()), long.Parse(longRaceDistance.ToString())));
	}
}