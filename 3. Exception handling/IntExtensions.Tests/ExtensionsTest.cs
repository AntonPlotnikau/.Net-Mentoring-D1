using System;
using NUnit.Framework;

namespace IntExtensions.Tests
{
	[TestFixture]
	public class ExtensionsTest
	{
		[Test]
		public void ParseString_NullString_ArgumentNullException()
			=> Assert.Throws<ArgumentNullException>(() => Extensions.ParseString(null));

		[Test]
		public void ParseString_EmptyString_StringParseFormatException()
			=> Assert.Throws<StringParseFormatException>(() => Extensions.ParseString(""));

		[Test]
		public void ParseString_StringWithSpaces_StringParseFormatException()
			=> Assert.Throws<StringParseFormatException>(() => Extensions.ParseString("   54354"));

		[Test]
		public void ParseString_StringWithNonDigitChars_StringParseFormatException()
			=> Assert.Throws<StringParseFormatException>(() => Extensions.ParseString("54354abs4"));

		[Test]
		public void ParseString_StringExceedsIntSize_OverflowException()
			=> Assert.Throws<OverflowException>(() => Extensions.ParseString("5555555555555555"));

		[Test]
		public void ParseString_DoubleValue_StringParseFormatException()
			=> Assert.Throws<StringParseFormatException>(() => Extensions.ParseString("54.354"));

		[Test]
		[TestCase(int.MaxValue, ExpectedResult = int.MaxValue)]
		[TestCase(int.MinValue, ExpectedResult = int.MinValue)]
		[TestCase(0, ExpectedResult = 0)]
		public int ParseString_IntStringValue_IntResult(int source)
			=> Extensions.ParseString(source.ToString());
	}
}
