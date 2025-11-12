using FluentValidation;
using LegacyOrderService.Models;
using LegacyOrderService.Services;
using Microsoft.Extensions.Logging;

namespace LegacyOrderService
{
    public class OrderProcessor
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IValidator<Order> _orderValidator;
        private readonly ILogger<OrderProcessor> _logger;

        public OrderProcessor(
            IOrderService orderService,
            IProductService productService,
            IValidator<Order> orderValidator,
            ILogger<OrderProcessor> logger)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _orderValidator = orderValidator ?? throw new ArgumentNullException(nameof(orderValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            try
            {
                var order = new Order();
                Console.WriteLine("Welcome to Order Processor!");
                Console.Write("Enter customer name: ");
                order.CustomerName = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter product name: ");
                order.ProductName = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Invalid quantity. Please enter a valid number.");
                    return;
                }

                order.Quantity = quantity;

                // Validate order
                var result = await _orderValidator.ValidateAsync(order);
                if (!result.IsValid)
                {
                    Console.WriteLine("\n Validation order failed");
                    foreach (var err in result.Errors)
                    {
                        Console.WriteLine($" - {err.ErrorMessage}");
                    }
                    return;
                }

                order.Price = await _productService.GetProductPriceAsync(order.ProductName);
                order.Total = order.Price * order.Quantity;

                Console.WriteLine("Processing order...");
                _ = await _orderService.CreateOrder(order);

                Console.WriteLine("Order completed!");
                Console.WriteLine($"Customer: {order.CustomerName}");
                Console.WriteLine($"Product: {order.ProductName}");
                Console.WriteLine($"Quantity: {order.Quantity}");
                Console.WriteLine($"Total: {order.Total}");
                Console.WriteLine($"Done.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the order.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
