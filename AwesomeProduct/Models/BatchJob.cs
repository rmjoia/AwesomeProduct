namespace AwesomeProduct.Models
{
    public class BatchJob
    {
        public BatchJob(int batchNumber, int number)
        {
            Batch = batchNumber;
            Number = number;
        }
        public int Batch { get; init; }
        public int Number { get; init; }
    }
}
