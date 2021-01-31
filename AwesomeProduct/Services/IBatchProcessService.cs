using AwesomeProduct.Models;
using AwesomeProduct.Persistence.Models;

namespace AwesomeProduct.Services
{
    public interface IBatchProcessService
    {
        BatchProcessResponse GetLast();
        int Insert(BatchProcess item);
    }
}
