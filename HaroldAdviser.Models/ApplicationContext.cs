using Microsoft.EntityFrameworkCore;

namespace HaroldAdviser.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<LogModel> Logs { get; set; }

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
