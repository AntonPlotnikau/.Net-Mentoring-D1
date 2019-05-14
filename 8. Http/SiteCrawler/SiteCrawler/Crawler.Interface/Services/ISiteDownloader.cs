using System.Threading.Tasks;

namespace Crawler.Interface.Services
{
    public interface ISiteDownloader
    {
        Task DownloadSiteAsync(string url, int deepLevel);
    }
}
