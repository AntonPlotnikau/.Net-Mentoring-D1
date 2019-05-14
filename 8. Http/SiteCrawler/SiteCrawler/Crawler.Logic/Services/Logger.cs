using static System.Console;
using Crawler.Interface.Services;

namespace Crawler.Logic.Services
{
    public class Logger : ILogger
    {
        private bool isEnabled;

        public Logger(bool isEnabled = true)
        {
            this.isEnabled = isEnabled;
        }

        public void Log(string message)
        {
            if (!isEnabled)
                return;

            WriteLine(message);
        }
    }
}
