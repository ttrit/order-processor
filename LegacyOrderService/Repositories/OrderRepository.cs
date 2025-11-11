using LegacyOrderService.Persistences;
using LegacyOrderService.Persistences.DbModels;

namespace LegacyOrderService.Data
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task SaveAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
