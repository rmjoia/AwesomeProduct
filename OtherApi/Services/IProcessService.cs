using System.Net.Http;
using System.Threading.Tasks;

namespace OtherApi.Services
{
    public interface IProcessService
    {
        Task<HttpResponseMessage> StartProcessingAsync(int numberOfBatchs, int numbersToProcess);
        Task<HttpResponseMessage> GetCurrentStatusAsync();
    }
}
