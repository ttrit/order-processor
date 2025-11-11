using LegacyOrderService.Persistences;
using LegacyOrderService.Persistences.DbModels;

namespace LegacyOrderService.Data
{
    public interface IOrderRepository
    {
    }

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
