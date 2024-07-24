using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Repo.Data;
using Repo.Repositories;
using Services.Interfaces;
using Services.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

var databaseType = builder.Configuration["DatabaseType"];


if (databaseType == "SqlServer")
{
    builder.Services.AddDbContext<MyDbContext>
        (item => item.UseSqlServer(builder.Configuration.GetConnectionString("Con"),
         x => x.MigrationsAssembly("Repo")));
}
else if (databaseType == "InMemory")
{
    builder.Services.AddDbContext<MyDbContext>
    (o => o.UseInMemoryDatabase("EFCoreOptimizationSamplesDB"));
}


builder.Services.AddScoped<DataSeeder>();
builder.Services.AddHostedService<SeedDataBackgroundService>();


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    if (dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
    {
        dbContext.Database.Migrate();
    }
}

app.Run();
