using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	public static class MathUtils
	{
		public static int GCD(int a, int b)
		{
			while (a != 0 && b != 0)
			{
				if (a > b)
					a %= b;
				else
					b %= a;
			}

			return a | b;
		}
		public static int GCD(IEnumerable<int> values)
		{
			if (!values.Any())
				return 0;
			if (values.Count() == 1)
				return values.First();
			return values.Aggregate(GCD);
		}
		public static byte GCD(byte a, byte b)
		{
			while (a != 0 && b != 0)
			{
				if (a > b)
					a %= b;
				else
					b %= a;
			}

			return (byte)(a | b);
		}
		public static byte GCD(IEnumerable<byte> values)
		{
			if (!values.Any())
				return 0;
			if (values.Count() == 1)
				return values.First();
			return values.Aggregate(GCD);
		}
		public static long GCD(long a, long b)
		{
			while (a != 0 && b != 0)
			{
				if (a > b)
					a %= b;
				else
					b %= a;
			}

			return (long)(a | b);
		}
		public static long GCD(IEnumerable<long> values)
		{
			if (!values.Any())
				return 0;
			if (values.Count() == 1)
				return values.First();
			return values.Aggregate(GCD);
		}

		public static int LCM(int a, int b) => a * (b / GCD(a, b));
		public static int LCM(IEnumerable<int> values)
		{
			if (!values.Any())
				return 0;
			if (values.Count() == 1)
				return values.First();
			return values.Aggregate(LCM);
		}

		public static int LCM(byte a, byte b) => a * (b / GCD(a, b));
		public static int LCM(IEnumerable<byte> values)
		{
			if (!values.Any())
				return 0;
			if (values.Count() == 1)
				return values.First();
			IEnumerable<int> intValues = values.Select(i=>(int)i).ToList();
			return intValues.Aggregate(LCM);
		}

		public static long LCM(long a, long b) => a * (b / GCD(a, b));
		public static long LCM(IEnumerable<long> values)
		{
			if (!values.Any())
				return 0;
			if (values.Count() == 1)
				return values.First();
			return values.Aggregate(LCM);
		}
	}
}
