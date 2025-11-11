using LegacyOrderService.Persistences;
using LegacyOrderService.Persistences.DbModels;
using Microsoft.EntityFrameworkCore;

namespace LegacyOrderService.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<decimal> GetPriceAsync(string productName)
        {
            var product = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == productName);

            if (product == null)
                throw new KeyNotFoundException($"Product '{productName}' not found in database.");

            return product.Price;
        }
    }
}
