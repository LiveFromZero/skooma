using Microsoft.EntityFrameworkCore;
using skooma_backend.Models;

namespace skooma_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Launch> Launches { get; set; }
        public DbSet<MoonData> MoonData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configurations can be added here
        }
    }
}
