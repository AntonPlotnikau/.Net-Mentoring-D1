using System;

namespace BLL
{
    public class HelloService
    {
		public string SayHello(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return $"{DateTime.Now} Hello, Anonym!";
			}

			return $"{DateTime.Now} Hello, {name}!";
		}
    }
}
