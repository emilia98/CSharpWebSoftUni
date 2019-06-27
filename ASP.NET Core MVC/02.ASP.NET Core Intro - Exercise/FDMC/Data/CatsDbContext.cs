using FDMC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace FDMC.Data
{
    public class CatsDbContext : DbContext
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options) : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>().ToTable("Cat");
        }
    }
}
