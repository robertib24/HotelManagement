using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Client.Services
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            return client.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            });
        }
    }
}