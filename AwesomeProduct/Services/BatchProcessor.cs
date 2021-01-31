using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeProduct.Models;

namespace AwesomeProduct.Services
{
    public class BatchProcessor : IBatchProcessor
    {
        private IGeneratorManager _processor;
        private ConcurrentBag<BatchJob> _currentBatchProcess;
        public BatchProcessor(IGeneratorManager processor)
        {
            _processor = processor;
            _processor.NumberGenerated += _processor_NumberGenerated;

            ReseBatchJobs();
        }

        private void ReseBatchJobs()
        {
            _currentBatchProcess = new ConcurrentBag<BatchJob>();
        }

        public BatchProcessResponse getStatus()
        {
            var currentBatchProcess = new List<BatchJob>(_currentBatchProcess.ToArray());
            var groupedBatchJobs = currentBatchProcess.GroupBy(b => b.BatchNumber).Select(g => new BatchJob(g.First().BatchNumber, g.Sum(b => b.Number), g.First().LeftToProcess - g.Count())).ToList();
            var isComplete = groupedBatchJobs.Any() && groupedBatchJobs.All(b => b.LeftToProcess == 0);

            var result = new BatchProcessResponse()
            {
                Data = groupedBatchJobs,
                IsComplete = isComplete,
                DateCompleted = isComplete ? new DateTime() : null
            };

            return result;
        }

        public BatchProcessResponse Process(int numberOrBatches, int numbersToProcess)
        {
            ReseBatchJobs();

            var process = Parallel.For(0, numberOrBatches, i =>
              {
                  var currentBatch = i + 1;
                  _processor.Generate(currentBatch, numbersToProcess);
              });

            return new BatchProcessResponse()
            {
                Data = new List<BatchJob>(_currentBatchProcess.ToArray()),
                IsComplete = process.IsCompleted
            };
        }

        private void _processor_NumberGenerated(BatchJob job)
        {
            _currentBatchProcess.Add(_processor.Multiply(job));
        }
    }
}
