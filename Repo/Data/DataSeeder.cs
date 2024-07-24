using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data
{
    public class DataSeeder(MyDbContext context, ILogger<DataSeeder> logger)
    {
        public async Task SeedDefaultIfNeeded()
        {
            // Check if the data already exists
            if (!context.Customers.Any() && !context.Products.Any() && !context.Orders.Any())
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
                await context.Customers.AddRangeAsync(customers);
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();

                // Create orders
                var orders = Enumerable.Range(1, 100000).Select(i => new Order
                {
                    CustomerId = customers[random.Next(customers.Count)].Id,
                    ProductId = products[random.Next(products.Count)].Id,
                    OrderDate = DateTime.Now.AddDays(-random.Next(1000))
                }).ToList();

                // Add orders to the context
                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();

                logger.LogInformation("Seeded the database with initial data.");
            }
            else
            {
                logger.LogInformation("Database already contains data.");
            }
        }
    }
}
