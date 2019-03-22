using System;
using StringExtensions;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				string source = Console.ReadLine();
				try
				{
					Console.WriteLine(source.GetFirstNonWhiteSpaceChar());
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

		}
	}
}
