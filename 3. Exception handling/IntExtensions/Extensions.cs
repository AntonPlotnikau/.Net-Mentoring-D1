using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntExtensions
{
    public static class Extensions
    {
		public static int ParseString(string source)
		{
			if (source == null) 
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (source.Length == 0) 
			{
				throw new StringParseFormatException($"this string is empty");
			}

			if (source.First() == '-')
			{
				return TransformStringToInt(source.Skip(1), -1);
			}

			return TransformStringToInt(source, 1);
		}

		private static int TransformStringToInt(IEnumerable<char> source, int sign)
		{
			int result = 0;

			foreach (var value in source)
			{
				var digit = TransformCharToDigit(value);
				result = AddDigitToResult(result, digit, sign);
			}

			return result;
		}

		private static int AddDigitToResult(int result, int digit, int sign)
		{
			checked
			{
				result = result*10 + sign*digit;
			}

			return result;
		}

		private static int TransformCharToDigit(char value)
		{
			if (!(value >= '0' && value <= '9'))
			{
				throw new StringParseFormatException($"Source string contains non digit chars");
			}

			return value - '0';
		}
    }
}
