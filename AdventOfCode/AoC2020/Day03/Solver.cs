using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Interfaces;

namespace AoC2020.Day03
{
	public class Solver : AbstractSolver<bool[,], int?, long?>
	{
		protected override string SolutionTextA => $"With a slope of {Slope} {SolutionValueA} trees are encountered" ;
		protected override string SolutionTextB => $"The combined number of encounters is {SolutionValueB}";

		public (int,int) Slope {
			get => _slope;
			set
			{
				_slope = value;
				solved = false;
			}
		}
		private (int,int) _slope;

		public IEnumerable<(int,int)> SlopesToCheck
		{
			get => _slopesToCheck;
			set
			{
				_slopesToCheck = value;
				solved = false;
			}
		}
		private IEnumerable<(int, int)> _slopesToCheck = new List<(int,int)>();

		public Solver(IInputDataConverter<bool[,]> inputDataConverter, IFileReader fileReader) : base(inputDataConverter, fileReader) { }
		protected override void SolveImplemented()
		{
			if (Slope.Item2 != 0)
			SolutionValueA = CountTreeEncounters(Slope);
			if (SlopesToCheck.Any())
			{
				SolutionValueB = 1;
				foreach ((int, int) slope in SlopesToCheck)
					SolutionValueB *= CountTreeEncounters(slope);
			}
		}
		private int CountTreeEncounters((int,int) slope)
		{
			int x = 0;
			int y = 0;
			int result = 0;
			while (y < inputData.GetLength(1))
			{
				if (inputData[x, y])
					result++;
				y += slope.Item2;
				x += slope.Item1;
				x %= inputData.GetLength(0);
			}
			return result;
		}
	}
}
