using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	public class InvalidInputException: Exception
	{
		public InvalidInputException() : base() { }
		public InvalidInputException(string message) : base(message) { }
	}
}
