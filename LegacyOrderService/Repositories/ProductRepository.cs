// Data/ProductRepository.cs
namespace LegacyOrderService.Data
{
    public interface IProductRepository
    {
        Task<decimal> GetPrice(string productName);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<string, decimal> _productPrices = new()
        {
            ["Widget"] = 12.99m,
            ["Gadget"] = 15.49m,
            ["Doohickey"] = 8.75m
        };

        public Task<decimal> GetPrice(string productName)
        {
            // Simulate an expensive lookup
            Thread.Sleep(500);

            if (_productPrices.TryGetValue(productName, out var price))
                return Task.FromResult(price);

            throw new Exception("Product not found");
        }
    }
}
