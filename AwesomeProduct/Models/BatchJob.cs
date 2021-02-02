namespace AwesomeProduct.Models
{
    public class BatchJob
    {
        public BatchJob(int batchNumber, int number, int leftToProcess = 0)
        {
            BatchNumber = batchNumber;
            LeftToProcess = leftToProcess;
            Number = number;
        }
        public int BatchNumber { get; init; }
        public int LeftToProcess { get; init; }
        public int Number { get; init; }
    }
}
