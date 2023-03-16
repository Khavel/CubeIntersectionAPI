using CubeIntersectionAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CubeIntersectionAPI.Infrastructure.Persistence
{
    public class CubeIntersectionContext : DbContext
    {
        public CubeIntersectionContext(DbContextOptions<CubeIntersectionContext> options)
            : base(options)
        {
        }
        public DbSet<Cube> Cubes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cube>().HasKey(c => c.Id);
            modelBuilder.Entity<Cube>().OwnsOne(c => c.Center);
            modelBuilder.Entity<Cube>().Property(c => c.SideLength).IsRequired();
        }
    }
}