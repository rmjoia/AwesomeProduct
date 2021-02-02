using System;
namespace AwesomeProduct.Persistence.Models
{
    public class Batch
    {
        public Guid Id { get; set; }
        public int BatchNumber { get; init; }
        public int LeftToProcess { get; init; }
        public int Number { get; init; }
    }
}
