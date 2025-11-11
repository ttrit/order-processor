using FluentValidation;
using LegacyOrderService.Data;
using LegacyOrderService.Models;
using LegacyOrderService.Persistences;
using LegacyOrderService.Persistences.UnitOfWork;
using LegacyOrderService.Services;
using LegacyOrderService.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LegacyOrderService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Configuration.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true);
            var connectionString = builder.Configuration.GetConnectionString("OrdersDb");

            InitializeDependencies(builder, connectionString);

            using var host = builder.Build();
            var orderProcessor = host.Services.GetRequiredService<OrderProcessor>();
            await orderProcessor.RunAsync();
        }

        static void InitializeDependencies(HostApplicationBuilder builder, string connectionString)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString), ServiceLifetime.Singleton);

            // AutoMapper configuration
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Models.Order, Persistences.DbModels.Order>().ReverseMap();
            });

            // Register services and repositories
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddTransient<IValidator<Order>, OrderValidator>();

            builder.Services.AddTransient<OrderProcessor>();
        }
    }
}
