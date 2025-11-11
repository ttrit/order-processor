using LegacyOrderService.Models;
using LegacyOrderService.Services;

namespace LegacyOrderService
{
    public class OrderProcessor
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderProcessor(
            IOrderService orderService,
            IProductService productService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task RunAsync()
        {
            var order = new Order();
            Console.WriteLine("Welcome to Order Processor!");
            Console.WriteLine("Enter customer name: ");
            order.CustomerName = Console.ReadLine();

            Console.WriteLine("Enter product name: ");
            order.ProductName = Console.ReadLine();

            order.Price = await _productService.GetPrice(order.ProductName);

            Console.WriteLine("Enter quantity: ");
            order.Quantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Processing order...");

            var createdOrder = await _orderService.CreateOrder(order);

            Console.WriteLine("Order completed!");
            Console.WriteLine($"Customer: {order.CustomerName}");
            Console.WriteLine($"Product: {order.ProductName}");
            Console.WriteLine($"Quantity: {order.Quantity}");
            Console.WriteLine($"Done.");
        }
    }
}
