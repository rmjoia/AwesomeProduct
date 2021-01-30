using System.Collections.Generic;

namespace AwesomeProduct.Models
{
    public class BatchProcessResponse
    {
        public List<BatchJob> Data { get; set; }
        public bool IsComplete { get; set; }
    }
}
