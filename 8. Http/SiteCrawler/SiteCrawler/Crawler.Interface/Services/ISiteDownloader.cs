using Crawler.Interface.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler.Interface.Services
{
    public interface ISiteDownloader
    {
		List<IConstraint> Constraints { get; }

        Task DownloadSiteAsync(string url, int deepLevel);
    }
}
