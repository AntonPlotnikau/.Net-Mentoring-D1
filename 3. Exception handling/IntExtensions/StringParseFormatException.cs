using System;

namespace IntExtensions
{
	public class StringParseFormatException : FormatException
	{
		public StringParseFormatException() : base() { }

		public StringParseFormatException(string message) : base(message) { }
	}
}
