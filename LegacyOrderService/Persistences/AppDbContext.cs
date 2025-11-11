using LegacyOrderService.Persistences.DbModels;
using Microsoft.EntityFrameworkCore;

namespace LegacyOrderService.Persistences
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();

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

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Widget", Price = 12.99m },
                new Product { Id = 2, Name = "Gadget", Price = 15.49m },
                new Product { Id = 3, Name = "Doohickey", Price = 8.75m }
            );
        }
    }
}
