using System;
using LegacyOrderService.Models;
using LegacyOrderService.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LegacyOrderService.Services;

namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            InitializeDependencies(builder);

            using var host = builder.Build();
            var orderProcessor = host.Services.GetRequiredService<OrderProcessor>();
            orderProcessor.Run();
        }

        static void InitializeDependencies(HostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddTransient<OrderProcessor>();
        }
    }
}
