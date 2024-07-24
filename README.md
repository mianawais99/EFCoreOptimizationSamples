# EFCoreOptimizationSamples

## About the Project

This repository contains an ASP.NET Core Web API project demonstrating EF Core optimization techniques using Onion Architecture. The project consists of four sub-projects:

<ul>
  <li><strong>Domain</strong>: Contains the entity models.</li>
  <li><strong>Repo</strong>: Contains the DbContext, repository implementations, and data seeding logic.</li>
  <li><strong>Services</strong>: Contains service interfaces and implementations.</li>
  <li><strong>WebAPI</strong>: Contains the API controllers and startup configuration.</li>
</ul> 

## Project Structure

```plaintext
EFCoreOptimizationSamples
│
├── Domain
│   └── Models
│       ├── Customer.cs
│       ├── Product.cs
│       └── Order.cs
│
├── Repo
│   ├── Data
│   │   ├── MyDbContext.cs
│   │   ├── DataSeeder.cs
│   │   └── SeedDataBackgroundService.cs
│   ├── Migrations
│   └── Repositories
│       ├── IGenericRepository.cs
│       └── GenericRepository.cs
│
├── Services
│   ├── Interfaces
│   │   ├── ICustomerService.cs
│   │   ├── IProductService.cs
│   │   └── IOrderService.cs
│   └── Services
│       ├── CustomerService.cs
│       ├── ProductService.cs
│       └── OrderService.cs
│
└── WebAPI
    ├── Controllers
    │   ├── CustomersController.cs
    │   ├── ProductsController.cs
    │   └── OrdersController.cs
    ├── appsettings.json
    └── Program.cs

```

# sff

