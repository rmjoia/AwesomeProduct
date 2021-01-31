using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtherApi.Services;

namespace OtherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispatchController : ControllerBase
    {

        private readonly IProcessService _apiConsumerService;

        public DispatchController(IProcessService apiConsumerService)
        {
            _apiConsumerService = apiConsumerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int batches = 0, int numbersToProcess = 0)
        {
            if (batches <= 0 || numbersToProcess <= 0)
            {
                return BadRequest();
            }

            var response = await _apiConsumerService.StartProcessingAsync(batches, numbersToProcess);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("status/")]
        public async Task<IActionResult> GetStatus()
        {
            var response = await _apiConsumerService.GetCurrentStatusAsync();

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<dynamic>(responseStream);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
