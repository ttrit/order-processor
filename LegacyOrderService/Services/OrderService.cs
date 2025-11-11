using AutoMapper;
using LegacyOrderService.Models;
using LegacyOrderService.Persistences.UnitOfWork;

namespace LegacyOrderService.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var newOrder = _mapper.Map<Order, Persistences.DbModels.Order>(order);

            await _unitOfWork.Orders.AddAsync(newOrder);
            await _unitOfWork.SaveChangesAsync();
            return order;
        }
    }
}
