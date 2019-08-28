using System.Net.Http;
using System.Threading.Tasks;

namespace advancedbackend.services
{
    public interface IHttpClientFactoryWrapper
    {
        IHttpClientWrapper CreateClient();
    }

    public class HttpClientFactoryWrapper : IHttpClientFactoryWrapper
    {
        private IHttpClientFactory ClientFactory;
        public HttpClientFactoryWrapper(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public IHttpClientWrapper CreateClient()
        {
            var wrapped = ClientFactory.CreateClient();
            return new HttpClientWrapper(wrapped);
        }
    }

    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        HttpClient Client;

        public HttpClientWrapper(HttpClient client)
        {
            Client = client;
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) {
            return Client.SendAsync(request);
        }
    }
}