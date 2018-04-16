using System.Net;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Boss.Pim.Utils
{
    public class WebSrcUtil : ISingletonDependency
    {
        public async Task<string> GetToString(string url, Encoding specifiedEncoding = null)
        {
            specifiedEncoding = specifiedEncoding ?? Encoding.UTF8;

            var webClient = new WebClient();

            var responseBytes = await webClient.DownloadDataTaskAsync(url);

            var response = specifiedEncoding.GetString(responseBytes);

            return response;
        }
    }
}