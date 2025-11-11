using LegacyOrderService.Persistences;
using LegacyOrderService.Persistences.DbModels;

namespace LegacyOrderService.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<decimal> GetPriceAsync(string productName);
    }
}
