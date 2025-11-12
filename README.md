# 🧾 Order Processor Service

A clean, modular **.NET 8** application demonstrating a layered architecture for order and product management.  
This project integrates **Entity Framework Core**, **SQL Server**, **FluentValidation**, **AutoMapper**, **Repository + Unit of Work patterns**, and **structured logging** for robust maintainability and scalability.

---

## 🚀 Features

- **Entity Framework Core + SQL Server** for data persistence  
- **Repository & Unit of Work pattern** for clean data access abstraction  
- **AutoMapper** for seamless domain ↔ database model mapping  
- **FluentValidation** for robust input validation  
- **In-memory caching** for optimized product lookups (5-minute TTL)  
- **Microsoft.Extensions.Logging** for full observability and debugging  
- **Dockerized environment** including SQL Server container  
- **AppDbContext** with automatic database creation/migrations

---

## 🐳 Running with Docker

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/order-processor.git
   cd order-processor/LegacyOrderService
2. Build and start containers:
   ```bash
   docker compose up -d