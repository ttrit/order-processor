using LegacyOrderService.Persistences;
using LegacyOrderService.Persistences.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LegacyOrderService.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMemoryCache _cache;
        private readonly string _cachePrefix = "ProductPrice_";

        public ProductRepository(AppDbContext context, IMemoryCache cache) : base(context)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<decimal> GetPriceAsync(string productName)
        {
            var cacheKey = $"{_cachePrefix}{productName.ToLower()}";

            if (_cache.TryGetValue(cacheKey, out decimal cachedPrice))
            {
                return cachedPrice;
            }

            var product = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == productName);

            if (product == null)
                throw new KeyNotFoundException($"Product '{productName}' not found in database.");

            _cache.Set(cacheKey, product.Price, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            });

            return product.Price;
        }
    }
}
