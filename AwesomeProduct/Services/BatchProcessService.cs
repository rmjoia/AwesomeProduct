using System;
using AwesomeProduct.Persistence;
using AwesomeProduct.Persistence.Models;

namespace AwesomeProduct.Services
{
    public class BatchProcessService : IBatchProcessService
    {
        private readonly IRepository<BatchProcess> _batchProcessesRepository;

        public BatchProcessService(IRepository<BatchProcess> batchProcessesRepository)
        {
            _batchProcessesRepository = batchProcessesRepository;
        }

        public BatchProcess GetLast()
        {
            return _batchProcessesRepository.GetLast();
        }

        public int Insert(BatchProcess item)
        {
            return _batchProcessesRepository.Insert(item);
        }
    }
}
