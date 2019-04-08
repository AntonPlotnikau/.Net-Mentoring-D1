using FileDistributor.Models;

namespace FileDistributor.Interfaces
{
	public interface ILogger
	{
		void Log(string message);

		void Log(string message, LoggingLevel level);
	}
}
