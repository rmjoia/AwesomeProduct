using System;
using AwesomeProduct.Models;
using AwesomeProduct.Persistence;
using AwesomeProduct.Persistence.Models;
using System.Linq;

namespace AwesomeProduct.Services
{
    public class BatchProcessService : IBatchProcessService
    {
        private readonly IRepository<BatchProcess> _batchProcessesRepository;

        public BatchProcessService(IRepository<BatchProcess> batchProcessesRepository)
        {
            _batchProcessesRepository = batchProcessesRepository;
        }

        public BatchProcessResponse GetLast()
        {
            var result = _batchProcessesRepository.GetLast();
            return result == null ? null : toBatchProcessDataResponse(result);
        }

        private BatchProcessResponse toBatchProcessDataResponse(BatchProcess batchProcess)
        {
            return new BatchProcessResponse()
            {
                Data = batchProcess.Data.Select(d => new BatchJob(d.BatchNumber, d.Number, d.LeftToProcess)).ToList(),
                DateCompleted = batchProcess.DateCompleted,
                IsComplete = true
            };
        }

        public int Insert(BatchProcess item)
        {
            item.DateCompleted = DateTime.UtcNow;
            return _batchProcessesRepository.Insert(item);
        }
    }
}
