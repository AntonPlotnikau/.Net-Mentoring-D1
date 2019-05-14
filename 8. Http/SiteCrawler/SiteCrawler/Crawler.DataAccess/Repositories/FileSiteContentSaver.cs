using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Crawler.DataAccess.Interface.Repositories;

namespace Crawler.DataAccess.Repositories
{
    public class FileSiteContentSaver : ISiteContentSaver
    {
        private string filePath;

        public FileSiteContentSaver(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public async Task SaveSiteContentAsync(string url)
        {
            string html = await DownloadHtmlAsync(url);

            string path = GetFilePath(url);

            await SaveToFileAsync(path, html);
        }

        private async Task<string> DownloadHtmlAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        return result;
                    }
                }
            }
        }

        private async Task SaveToFileAsync(string path, string content)
        {
            using (var writer = new StreamWriter(path))
            {
                await writer.WriteAsync(content);
            }
        }

        private string GetFilePath(string url)
        {
            var chars = Path.GetInvalidFileNameChars();

            foreach (var item in chars)
            {
                url = url.Replace(item, '_');
            }

            return filePath + url;
        }
    }
}
