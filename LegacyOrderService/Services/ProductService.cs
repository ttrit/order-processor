using LegacyOrderService.Data;

namespace LegacyOrderService.Services
{
    public interface IProductService
    {
        Task<decimal> GetPrice(string productName);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<decimal> GetPrice(string productName)
        {
            return Convert.ToDecimal(await _productRepository.GetPrice(productName));
        }
    }
}
