using System;
using System.Collections.Generic;
using AwesomeProduct.Models;

namespace AwesomeProduct.Persistence.Models
{
    public class BatchProcess
    {
        public Guid Id { get; set; }
        public List<Batch> Data { get; set; }
        public DateTime? DateCompleted { get; set; }

        internal object Select(Func<object, BatchJob> p)
        {
            throw new NotImplementedException();
        }
    }
}
