using System;

namespace BLL
{
	public static class HelloService
	{
		public static string SayHello(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return $"{DateTime.Now} Hello, Anonym!";
			}

			return $"{DateTime.Now} Hello, {name}!";
		}
    }
}
