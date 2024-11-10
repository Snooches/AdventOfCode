using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Interfaces;

namespace Utilities.Interfaces
{
	public interface IInputDataConverter { }

	public interface IInputDataConverter<out T> : IInputDataConverter
	{
		public T ConvertInputData(IFileReader fileReader);
	}
}
