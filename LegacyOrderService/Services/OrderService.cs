using AutoMapper;
using LegacyOrderService.Data;
using LegacyOrderService.Models;

namespace LegacyOrderService.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var newOrder = _mapper.Map<Order, Persistences.DbModels.Order>(order);

            await _orderRepository.SaveAsync(newOrder);
            return order;
        }
    }
}
