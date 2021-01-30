using System.Threading.Tasks;
using AwesomeProduct.Models;
using AwesomeProduct.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProduct.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        private IBatchProcessor _processor;
        public ProcessController(IBatchProcessor batchProcessor)
        {
            _processor = batchProcessor;
        }

        [HttpGet]
        public IActionResult Get(int batches = 0, int numbersToProcess = 0)
        {
            if (batches <= 0 || numbersToProcess <= 0)
            {
                return BadRequest();
            }

            Task.Run(() =>
            {
                _processor.Process(batches, numbersToProcess);
            });

            return Ok();
        }

        [HttpGet("status/")]
        public BatchProcessResponse GetStatus()
        {
            return _processor.getStatus();
        }
    }
}
