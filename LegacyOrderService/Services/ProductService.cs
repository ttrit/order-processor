using LegacyOrderService.Data;

namespace LegacyOrderService.Services
{
    public interface IProductService
    {
        decimal GetPrice(string productName);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public decimal GetPrice(string productName)
        {
            return Convert.ToDecimal(_productRepository.GetPrice(productName));
        }
    }
}
