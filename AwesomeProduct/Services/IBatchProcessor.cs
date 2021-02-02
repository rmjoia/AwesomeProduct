using AwesomeProduct.Models;

namespace AwesomeProduct.Services
{
    public interface IBatchProcessor
    {
        public BatchProcessResponse Process(int numberOrBatches, int numbersToProcess);
        public BatchProcessResponse getStatus();
    }
}
