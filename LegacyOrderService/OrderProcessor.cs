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

        public void Run()
        {
            Console.WriteLine("Welcome to Order Processor!");
            Console.WriteLine("Enter customer name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter product name: ");
            string productName = Console.ReadLine();

            decimal price = _productService.GetPrice(productName);

            Console.WriteLine("Enter quantity: ");
            int qty = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Processing order...");

            var order = _orderService.CreateOrder(name, productName, qty, price);

            Console.WriteLine("Order completed!");
            Console.WriteLine($"Customer: {order.CustomerName}");
            Console.WriteLine($"Product: {order.ProductName}");
            Console.WriteLine($"Quantity: {order.Quantity}");
            Console.WriteLine($"Done.");
        }
    }
}
