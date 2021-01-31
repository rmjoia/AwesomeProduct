using System.Linq;
using AwesomeProduct.Models;
using AwesomeProduct.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace AwesomeProduct.Persistence
{
    public class BatchProcessesRepository : IRepository<BatchProcess>
    {
        private readonly AwesomeProductDbContext _context;

        public BatchProcessesRepository(AwesomeProductDbContext context)
        {
            _context = context;
        }

        public BatchProcess GetLast()
        {
            return _context.BatchProcesses
                .OrderByDescending(b => b.DateCompleted)
                .Take(1)
                .Include(j => j.Data)
                .FirstOrDefault();
        }

        public int Insert(BatchProcess item)
        {
            _context.BatchProcesses.Add(item);
            return _context.SaveChanges();
        }
    }
}
