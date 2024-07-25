# EFCoreOptimizationSamples

## About the Project

This repository contains an ASP.NET Core Web API project demonstrating EF Core optimization techniques using Onion Architecture. The project consists of four sub-projects:

<ul>
  <li><strong>Domain</strong>: Contains the entity models.</li>
  <li><strong>Repo</strong>: Contains the DbContext, repository implementations, and data seeding logic.</li>
  <li><strong>Services</strong>: Contains service interfaces and implementations.</li>
  <li><strong>WebAPI</strong>: Contains the API controllers and startup configuration.</li>
</ul> 

## Features

- **Initial Data Seeding**: The application seeds the database with the following records:
  - **Customer**: 10,000 records
  - **Product**: 5,000 records
  - **Order**: 100,000 records
- **Database Compatibility**: The application can work with both InMemory and SQL Server databases.
- **Logging**: Log the time taken for batch and non-batch operations.
- **Benchmarking**: Measure and compare the performance of different optimization techniques.

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

## Setting Up the Project Locally

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/mianawais99/EFCoreOptimizationSamples.git
    cd EFCoreOptimizationSamples
    ```

2. Restore the dependencies:

    ```bash
    dotnet restore
    ```

3. Build the project:

    ```bash
    dotnet build
    ```

### Configuration

1. **Update Connection String**:
   - Open `appsettings.json` in the `WebAPI` project.
   - Update the connection string to point to your SQL Server instance.

2. **Run Migrations**:
   ```bash
   dotnet ef migrations add InitialCreate --project Repo --startup-project WebAPI
   dotnet ef database update --project Repo --startup-project WebAPI 
   ```

### Run the Application
   ```bash
   dotnet run --project WebAPI 
   ```
