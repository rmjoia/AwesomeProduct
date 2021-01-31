using AwesomeProduct.Persistence.Models;

namespace AwesomeProduct.Services
{
    public interface IBatchProcessService
    {
        BatchProcess GetLast();
        int Insert(BatchProcess item);
    }
}
