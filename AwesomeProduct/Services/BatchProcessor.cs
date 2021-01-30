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
        private bool _processingStatus;
        public BatchProcessor(IGeneratorManager processor)
        {
            _processor = processor;
            _processor.NumberGenerated += _processor_NumberGenerated;

            ReseBatchJobs();
        }

        private void ReseBatchJobs()
        {
            _currentBatchProcess = new ConcurrentBag<BatchJob>();
            _processingStatus = false;
        }

        public BatchProcessResponse getStatus()
        {
            var currentBatchProcess = new List<BatchJob>(_currentBatchProcess.ToArray());
            var groupedBatchJobs = currentBatchProcess.GroupBy(b => b.Batch).Select(g => new BatchJob(g.First().Batch, g.Sum(b => b.Number))).ToList();
            return new BatchProcessResponse()
            {
                Data = groupedBatchJobs,
                IsComplete = _processingStatus
            };
        }

        public BatchProcessResponse Process(int numberOrBatches, int numbersToProcess)
        {
            ReseBatchJobs();

            var process = Parallel.For(0, numberOrBatches, i =>
              {
                  var currentBatch = i + 1;
                  _processor.Generate(currentBatch, numbersToProcess);
              });

            _processingStatus = process.IsCompleted;

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
