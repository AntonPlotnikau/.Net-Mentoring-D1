using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtensions.Tests
{
	[TestFixture]
	public class ExtensionsTest
	{
		[Test]
		public void GetFirstNonWhiteSpaceChar_NullString_ArgumentException()
			=> Assert.Throws<ArgumentException>(() => Extensions.GetFirstNonWhiteSpaceChar(null));

		[Test]
		public void GetFirstNonWhiteSpaceChar_EmptyString_ArgumentException()
			=> Assert.Throws<ArgumentException>(() => Extensions.GetFirstNonWhiteSpaceChar(string.Empty));

		[Test]
		public void GetFirstNonWhiteSpaceChar_WhiteSpaceString_ArgumentException()
			=> Assert.Throws<ArgumentException>(() => Extensions.GetFirstNonWhiteSpaceChar("       "));

		[Test]
		[TestCase("abcderrtr", ExpectedResult = 'a')]
		[TestCase("1fdsf2df4fg", ExpectedResult = '1')]
		[TestCase("      Test   ", ExpectedResult = 'T')]
		[TestCase("				Test   ", ExpectedResult = 'T')]
		[TestCase("_anton", ExpectedResult = '_')]
		public char GetFirstNonWhiteSpaceChar_ValidSourceString_ReturnsFirstNonWhiteSpaceChar(string source)
			=> source.GetFirstNonWhiteSpaceChar();
	}
}
