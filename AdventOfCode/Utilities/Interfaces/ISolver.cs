using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Interfaces
{
	public interface ISolver
	{
		public void Solve();
		public string SolutionA { get; }
		public string SolutionB { get; }
	}
}
