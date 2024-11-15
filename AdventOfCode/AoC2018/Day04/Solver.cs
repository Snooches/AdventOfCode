
using System.Linq;
using Utilities;
using Utilities.Interfaces;

namespace AoC2018.Day04;

internal class Solver(IInputDataConverter<IEnumerable<GuardSleepSchedule>> inputDataConverter, IFileReader fileReader)
			 : AbstractSolver<IEnumerable<GuardSleepSchedule>, long?, long?>(inputDataConverter, fileReader)
{
	protected override string SolutionTextA => $"Going with strategy 1 the answer is: {SolutionValueA}";
	protected override string SolutionTextB => $"Going with strategy 2 the answer is: {SolutionValueB}";

	protected override void SolveImplemented()
	{
		int mostSleepTimeByGuard = 0;
		int mostSleepTimeByMinute = 0;
		foreach(GuardSleepSchedule sleepSchedule in inputData)
		{
			int totalSleepTime = 0;
			int mostSleptMinute = 0;
			int sleepAmountOfMostSleptMinute = 0;
			for (int minute = 0; minute < 60; minute++)
			{
				totalSleepTime += sleepSchedule.Schedule[minute];
				if (sleepSchedule.Schedule[minute] > sleepAmountOfMostSleptMinute)
				{
					sleepAmountOfMostSleptMinute = sleepSchedule.Schedule[minute];
					mostSleptMinute = minute;
				}
			}
			if (totalSleepTime > mostSleepTimeByGuard)
			{
				mostSleepTimeByGuard = totalSleepTime;
				SolutionValueA = sleepSchedule.Id * mostSleptMinute;
			}
			if(sleepAmountOfMostSleptMinute> mostSleepTimeByMinute)
			{
				mostSleepTimeByMinute = sleepAmountOfMostSleptMinute;
				SolutionValueB = sleepSchedule.Id * mostSleptMinute;
			}
		}
	}
}