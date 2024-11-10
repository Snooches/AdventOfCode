using Utilities.Interfaces;

namespace AoC2023.Day05;

internal class InputDataConverter : IInputDataConverter<(IEnumerable<long>, IEnumerable<SeedMapper>)>
{
	public (IEnumerable<long>, IEnumerable<SeedMapper>) ConvertInputData(IFileReader fileReader)
	{
		IEnumerable<long>? seeds = null;
		IList<SeedMapper> seedMappers = [];
		SeedMapper? currentMapper = null;
		foreach (string line in fileReader.ReadLines())
		{
			if (seeds is null)
			{
				seeds = line[7..].Split(' ').Select(x => long.Parse(x));
				continue;
			}

			if (string.IsNullOrWhiteSpace(line))
				continue;

			if (!byte.TryParse(line[0].ToString(), out byte _))
			{
				currentMapper = new();
				seedMappers.Add(currentMapper);
				continue;
			}

			long[] mappingParams = line.Split(' ').Select(x => long.Parse(x)).ToArray();
			currentMapper.AddMapping(mappingParams[0], mappingParams[1], mappingParams[2]);
		}

		return (seeds, seedMappers);
	}
}