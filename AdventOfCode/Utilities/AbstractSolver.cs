using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Interfaces;

namespace Utilities
{
	public abstract class AbstractSolver<InputType, SolutionTypeA, SolutionTypeB> : ISolver
	{
		protected InputType inputData;
		protected bool solved;
		public SolutionTypeA? SolutionValueA { get; protected set; }
		public SolutionTypeB? SolutionValueB { get; protected set; }

		protected virtual String SolutionTextA => SolutionValueA?.ToString() ?? String.Empty;
		public string SolutionA => solved ? this.SolutionTextA : "Puzzle has not been solved yet.";

		protected virtual String SolutionTextB => SolutionValueB?.ToString() ?? String.Empty;
		public string SolutionB => solved ? this.SolutionTextB : "Puzzle has not been solved yet.";
		protected abstract void SolveImplemented();

		public void Solve()
		{
			SolveImplemented();
			solved = true;
		}

        protected AbstractSolver(IInputDataConverter<InputType> inputDataConverter, IFileReader fileReader)
		{ 
			this.inputData = inputDataConverter.ConvertInputData(fileReader);
			this.solved = false;
		}
	}
}
