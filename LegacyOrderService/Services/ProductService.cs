using LegacyOrderService.Persistences.UnitOfWork;

namespace LegacyOrderService.Services
{
    public interface IProductService
    {
        Task<decimal> GetProductPriceAsync(string productName);
    }

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<decimal> GetProductPriceAsync(string productName)
        {
            return await _unitOfWork.Products.GetPriceAsync(productName);
        }
    }
}
