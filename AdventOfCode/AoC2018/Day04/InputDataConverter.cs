using System.Globalization;
using Utilities.Interfaces;

namespace AoC2018.Day04;

internal class InputDataConverter : IInputDataConverter<IEnumerable<GuardSleepSchedule>>
{
	public IEnumerable<GuardSleepSchedule> ConvertInputData(IFileReader fileReader)
	{
		SortedList<DateTime, String> events = [];
		foreach (string line in fileReader.ReadLines())
		{
			events.Add(DateTime.ParseExact(line[1..17], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), line[19..]);
		}

		VerifyInputAssumption(events);

		Dictionary<int, GuardSleepSchedule> schedules = [];
		GuardSleepSchedule? currentSchedule = null;
		int fellAsleepAt = 0;
		foreach ((DateTime time, String action) in events)
		{
			switch (action[0])
			{
				case 'G': //new shift
					int id = int.Parse(action.Split(" ")[1][1..]);
					if (!schedules.TryGetValue(id, out currentSchedule))
					{
						currentSchedule = new GuardSleepSchedule() { Id = id };
						schedules[id] = currentSchedule;
					}
					break;
				case 'f': //Guard falls asleep
					fellAsleepAt = time.Minute;
					break;
				case 'w': //Guard wakes up
					for (int i = fellAsleepAt; i < time.Minute; i++)
						currentSchedule!.Schedule[i]++;
					break;
			}
		}
		return schedules.Values;
	}

	//The assumption here is, that each shift start with a guard showing up followed by alternating falling asleep and waking up
	//always starting with falling asleep and ending with waking up
	private static void VerifyInputAssumption(SortedList<DateTime, String> input)
	{
		Boolean asleep = false;
		foreach ((DateTime _, String action) in input)
		{
			switch (action[0])
			{
				case 'G':
					if (asleep)
						throw new InvalidOperationException("Guard start shift while asleep.");
					break;
				case 'f':
					if (asleep)
						throw new InvalidOperationException("Guard falls asleep while already sleeping.");
					else
						asleep = true;
					break;
				case 'w':
					if (asleep)
						asleep = false;
					else
						throw new InvalidOperationException("Guard wakes up while already awake.");
					break;
				default:
					throw new InvalidOperationException($"unknown action '{action}'.");
			}
		}
		if (asleep)
			throw new InvalidOperationException("Last guard ended shift while asleep.");
	}
}