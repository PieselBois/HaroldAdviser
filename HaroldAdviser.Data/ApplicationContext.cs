using Microsoft.EntityFrameworkCore;

namespace HaroldAdviser.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}