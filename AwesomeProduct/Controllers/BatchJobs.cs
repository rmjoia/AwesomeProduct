using System;
using AwesomeProduct.Persistence.Models;
using AwesomeProduct.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeProduct.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BatchJobsController : ControllerBase
    {
        private IBatchProcessService _batchProcessService;
        public BatchJobsController(IBatchProcessService batchProcessesRepository)
        {
            _batchProcessService = batchProcessesRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
               var result = _batchProcessService.GetLast();

                if(result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
        public IActionResult Post(BatchProcess batchProcess)
        {
            try
            {
                var result = _batchProcessService.Insert(batchProcess);

                if(result == 0)
                {
                    return BadRequest();
                }

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
