using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data
{
    public class SeedDataBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SeedDataBackgroundService> _logger;

        public SeedDataBackgroundService(IServiceProvider serviceProvider, ILogger<SeedDataBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                    if (dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
                    {
                        // Seed data for SQL Server
                        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                        await dataSeeder.SeedDefaultIfNeeded();
                        _logger.LogInformation("Data seeding completed in Database");
                    }
                    else if (dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        // Seed data for InMemory
                        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                        await dataSeeder.SeedDefaultIfNeeded();
                        _logger.LogInformation("Data seeding completed in Memory");
                    }

                    //_logger.LogInformation("Data seeding completed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

    }
}
