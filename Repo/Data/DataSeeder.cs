using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data
{
    public class DataSeeder
    {
        private readonly MyDbContext _context;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(MyDbContext context, ILogger<DataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedDefaultIfNeeded()
        {
            // Check if the data already exists
            if (!_context.Customers.Any() && !_context.Products.Any() && !_context.Orders.Any())
            {
                // Sample data arrays
                var customerNames = new[] { "John Doe", "Jane Smith", "Robert Johnson", "Michael Brown", "William Davis" };
                var productNames = new[] { "Laptop", "Smartphone", "Tablet", "Headphones", "Smartwatch" };

                var random = new Random();

                // Create customers
                var customers = Enumerable.Range(1, 10000).Select(i => new Customer
                {
                    Name = customerNames[random.Next(customerNames.Length)],
                    Email = $"customer{i}@example.com",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                // Create products
                var products = Enumerable.Range(1, 5000).Select(i => new Product
                {
                    Name = productNames[random.Next(productNames.Length)],
                    Price = random.Next(100, 1000)
                }).ToList();

                // Add customers and products to the context
                await _context.Customers.AddRangeAsync(customers);
                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();

                // Create orders
                var orders = Enumerable.Range(1, 100000).Select(i => new Order
                {
                    CustomerId = customers[random.Next(customers.Count)].Id,
                    ProductId = products[random.Next(products.Count)].Id,
                    OrderDate = DateTime.Now.AddDays(-random.Next(1000))
                }).ToList();

                // Add orders to the context
                await _context.Orders.AddRangeAsync(orders);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeded the database with initial data.");
            }
            else
            {
                _logger.LogInformation("Database already contains data.");
            }
        }
    }
}
