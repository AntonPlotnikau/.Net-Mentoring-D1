using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Crawler.DataAccess.Interface.Repositories;
using Crawler.Interface.Services;
using HtmlAgilityPack;
using Crawler.Interface.Models;

namespace Crawler.Logic.Services
{
    public class SiteDownloader : ISiteDownloader
    {
        private ILogger logger;

        private ISiteContentSaver saver;

		public List<IConstraint> Constraints { get; private set; }

		public SiteDownloader(ILogger logger, ISiteContentSaver saver)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.saver = saver ?? throw new ArgumentNullException(nameof(saver));
			this.Constraints = new List<IConstraint>();
        }

        public async Task DownloadSiteAsync(string url, int deepLevel)
        {
            if (!IsUrlValid(url)) 
            {
                logger.Log($"This link {url} is not valid");
                return;
            }

            await SaveContentAsync(url);

            logger.Log($"Link with url {url} was saved.");

            IEnumerable<string> links = GetAllLinks(url);
            
            logger.Log($"Links count: {links.Count()}");

            if (deepLevel == 0)
            {
                return;
            }

            foreach (var link in links) 
            {
                await DownloadSiteAsync(link, deepLevel - 1);
            }
        }

        private async Task SaveContentAsync(string html)
        {
            await saver.SaveSiteContentAsync(html);
        }

        private IEnumerable<string> GetAllLinks(string url)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                yield return link.GetAttributeValue("href", null);
            }
        }

        private bool IsUrlValid(string url)
        {
            if (url == null)
                return false;

			if(!(Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
						&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
			{
				return false;
			}

			foreach (var constraint in Constraints)
			{
				if (!constraint.IsAccepted(url))
				{
					return false;
				}
			}

			return true;
        }
    }
}
