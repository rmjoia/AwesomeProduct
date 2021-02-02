using AwesomeProduct.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace AwesomeProduct.Persistence
{
    public class AwesomeProductDbContext : DbContext
    {
        public AwesomeProductDbContext(DbContextOptions<AwesomeProductDbContext> options) : base(options)
        {
        }

        public DbSet<Batch> BatchJobs { get; set; }
        public DbSet<BatchProcess> BatchProcesses { get; set; }
    }
}
