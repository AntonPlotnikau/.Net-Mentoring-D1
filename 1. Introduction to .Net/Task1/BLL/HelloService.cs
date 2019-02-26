using System;

namespace BLL
{
	public static class HelloService
	{
		public static string SayHello(DateTime date, string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return $"{date} Hello, Anonym!";
			}

			return $"{date} Hello, {name}!";
		}
    }
}
