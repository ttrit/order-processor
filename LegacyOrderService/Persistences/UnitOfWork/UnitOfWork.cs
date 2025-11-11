using LegacyOrderService.Persistences.DbModels;
using LegacyOrderService.Repositories;

namespace LegacyOrderService.Persistences.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Order> Orders { get; }
        IProductRepository Products { get; }

        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public IGenericRepository<Order> Orders { get; }
        public IProductRepository Products { get; }

        public UnitOfWork(
            AppDbContext dbContext,
            IGenericRepository<Order> orderRepository,
            IProductRepository productRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Orders = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            Products = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
