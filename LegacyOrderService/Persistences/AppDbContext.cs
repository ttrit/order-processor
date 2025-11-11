using LegacyOrderService.Persistences.DbModels;
using Microsoft.EntityFrameworkCore;

namespace LegacyOrderService.Persistences
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(o => o.Id);
                entity.Property(o => o.CustomerName).IsRequired();
                entity.Property(o => o.ProductName).IsRequired();
            });
        }
    }
}
