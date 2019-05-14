using System;
using System.Threading.Tasks;
using Crawler.DataAccess.Interface.Repositories;
using Crawler.DataAccess.Repositories;
using Crawler.Interface.Models;
using Crawler.Interface.Services;
using Crawler.Logic.Services;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            ISiteContentSaver saver = new FileSiteContentSaver(@"D:\");
            ILogger logger = new Logger();
            ISiteDownloader downloader = new SiteDownloader(logger, saver);

            await downloader.DownloadSiteAsync("https://msdn.microsoft.com/en-us/library/system.io.path.getinvalidfilenamechars(v=vs.110).aspx", 2);
        }
    }
}
