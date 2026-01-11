using Microsoft.EntityFrameworkCore;
using skooma_backend.Data.DBModels;

namespace skooma_backend.Data
{

    public class AppDbContext : DbContext
    {
        public DbSet<DBLocation> Locations => Set<DBLocation>();
        public DbSet<DBLaunch> Launches => Set<DBLaunch>();
        public  DbSet<DBMoonData> MoonData => Set<DBMoonData>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent configuration (optional: conventions already handle this).
            modelBuilder.Entity<DBLocation>()
                .HasMany(a => a.Launches)
                .WithOne(b => b.Location)
                .HasForeignKey(b => b.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
