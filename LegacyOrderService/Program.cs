using FluentValidation;
using LegacyOrderService.Models;
using LegacyOrderService.Persistences;
using LegacyOrderService.Persistences.UnitOfWork;
using LegacyOrderService.Repositories;
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
            var masterConnectionString = builder.Configuration.GetConnectionString("MasterDb");
            var orderConnectionString = builder.Configuration.GetConnectionString("OrdersDb");

            InitializeDatabase(builder, masterConnectionString, orderConnectionString);
            InitializeDependencies(builder);

            using var host = builder.Build();
            var orderProcessor = host.Services.GetRequiredService<OrderProcessor>();
            await orderProcessor.RunAsync();
        }

        static void InitializeDependencies(HostApplicationBuilder builder)
        {
            // AutoMapper configuration
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Order, Persistences.DbModels.Order>().ReverseMap();
            });

            builder.Services.AddLogging();

            builder.Services.AddMemoryCache();

            // Register services and repositories
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddTransient<IValidator<Order>, OrderValidator>();

            builder.Services.AddTransient<OrderProcessor>();
        }

        static void InitializeDatabase(HostApplicationBuilder builder, string masterConnectionString, string orderConnectionString)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(orderConnectionString));
        }
    }
}
