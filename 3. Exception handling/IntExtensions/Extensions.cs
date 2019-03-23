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
				return -1*TransformStringToInt(source.Skip(1).Reverse());
			}

			return TransformStringToInt(source.Reverse());
		}

		private static int TransformStringToInt(IEnumerable<char> source)
		{
			int result = 0;
			int coefficient = 0;

			foreach (var value in source)
			{
				result = result.AddDigitToResult(value, coefficient);

				coefficient++;
			}

			return result;
		}

		private static int AddDigitToResult(this int result, char value, int coefficient)
		{
			if (!Char.IsDigit(value))
			{
				throw new StringParseFormatException($"Source string contains non digit chars");
			}

			checked
			{
				result = result + (int)(Char.GetNumericValue(value) * Math.Pow(10, coefficient));
			}

			return result;
		}
    }
}
