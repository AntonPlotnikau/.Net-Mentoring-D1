using System;
using BLL;

namespace CoreConsoleUI
{
	class Program
	{
		static void Main(string[] args)
		{
			string name = args.Length > 0 ? args[0] : null;
			string output = HelloService.SayHello(DateTime.Now, name);
			Console.WriteLine(output);
		}
	}
}