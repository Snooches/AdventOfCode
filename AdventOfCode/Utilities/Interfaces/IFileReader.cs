﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Interfaces
{
	public interface IFileReader
	{
		public IEnumerable<string> ReadLines();
	}
}
