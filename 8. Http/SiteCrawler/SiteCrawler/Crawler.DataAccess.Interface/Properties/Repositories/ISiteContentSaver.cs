using System.Threading.Tasks;

namespace Crawler.DataAccess.Interface.Repositories
{
    public interface ISiteContentSaver
    {
        Task SaveSiteContentAsync(string url);
    }
}
