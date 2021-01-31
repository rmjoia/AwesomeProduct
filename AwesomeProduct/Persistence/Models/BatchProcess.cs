using System;
using System.Collections.Generic;

namespace AwesomeProduct.Persistence.Models
{
    public class BatchProcess
    {
        public Guid Id { get; set; }
        public List<Batch> Data { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
