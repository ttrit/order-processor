using LegacyOrderService.Data;
using LegacyOrderService.Models;

namespace LegacyOrderService.Services
{
    public interface IOrderService
    {
        Order CreateOrder(string customerName, string productName, int quantity, decimal price);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public Order CreateOrder(string customerName, string productName, int quantity, decimal price)
        {
            var newOrder = new Order
            {
                CustomerName = customerName,
                ProductName = productName,
                Quantity = quantity,
                Price = price
            };

            _orderRepository.Save(newOrder);
            return newOrder;
        }
    }
}
