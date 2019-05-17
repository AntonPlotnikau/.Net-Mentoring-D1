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
			IConstraint domainConstraint = new DomainConstraint(new Uri("https://github.com/").Host);
			var extensionConstraint = new ExtensionConstraint(new string[] { "png","jpg"});
			downloader.Constraints.Add(domainConstraint);
			downloader.Constraints.Add(extensionConstraint);
			await downloader.DownloadSiteAsync("https://github.com/", 2);
        }
    }
}
