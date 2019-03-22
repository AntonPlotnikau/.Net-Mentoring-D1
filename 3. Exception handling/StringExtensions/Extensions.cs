using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtensions
{
	public static class Extensions
	{
		public static char GetFirstNonWhiteSpaceChar(this string source)
		{
			if (string.IsNullOrWhiteSpace(source))
			{
				throw new ArgumentException($"this string is null, empty or white space");
			}

			return source.Trim(' ').First();
		}
	}
}
