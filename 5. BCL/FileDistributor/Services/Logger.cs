using FileDistributor.Interfaces;
using FileDistributor.Models;
using System;

namespace FileDistributor.Services
{
	public class Logger : ILogger
	{
		public void Log(string message)
		{
			this.Log(message, LoggingLevel.Info);
		}

		public void Log(string message, LoggingLevel level)
		{
			Console.WriteLine($"[{level}] {message}");
		}
	}
}
