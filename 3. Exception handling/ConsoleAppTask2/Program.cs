using System;
using IntExtensions;

namespace ConsoleAppTask2
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true) 
			{
				try
				{
					string source = Console.ReadLine();
					Console.WriteLine(Extensions.ParseString(source));
				}
				catch(ArgumentNullException)
				{
					Console.WriteLine("source string is null");
				}
				catch (StringParseFormatException ex)
				{
					Console.WriteLine(ex.Message);
				}
				catch(OverflowException ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
