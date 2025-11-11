using LegacyOrderService.Models;
using Microsoft.Data.Sqlite;

namespace LegacyOrderService.Data
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private string _connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, @"..\..\..\orders.db")}";


        public void Save(Order order)
        {
            var connection = new SqliteConnection(_connectionString);
            
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $@"
                INSERT INTO Orders (CustomerName, ProductName, Quantity, Price)
                VALUES ('{order.CustomerName}', '{order.ProductName}', {order.Quantity}, {order.Price})";

            command.ExecuteNonQuery();            
        }

        public void SeedBadData()
        {
            var connection = new SqliteConnection(_connectionString);            
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Orders (CustomerName, ProductName, Quantity, Price) VALUES ('John', 'Widget', 9999, 9.99)";
            cmd.ExecuteNonQuery();
            
        }
    }
}
