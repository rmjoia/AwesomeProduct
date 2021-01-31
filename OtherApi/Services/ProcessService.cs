using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OtherApi.Services
{
    public class ProcessService : IProcessService
    {
        const string PROCESSING_API_URL = "https://localhost:5001/";
        private IHttpClientFactory _clientFactory;

        public ProcessService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> StartProcessingAsync(int numberOfBatches, int numbersToProcess)
        {
            var url = $"{PROCESSING_API_URL}Process/?batches={numberOfBatches}&numbersToProcess={numbersToProcess}";
            var request = generateGetRequest(url);

            var client = _clientFactory.CreateClient();

            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetCurrentStatusAsync()
        {
            var url = $"{PROCESSING_API_URL}Process/Status/";
            var request = generateGetRequest(url);

            var client = _clientFactory.CreateClient();

            return await client.SendAsync(request);
        }

        private HttpRequestMessage generateGetRequest(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            return request;
        }
    }
}
