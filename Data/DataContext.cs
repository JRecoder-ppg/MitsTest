using Microsoft.EntityFrameworkCore;
using MitsTest.Models;

namespace MitsTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("tests");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        // DBSet for each Table/Class
        public DbSet<CultureCodeModel> CultureCodes { get; set; }
    }
}
