# Section 2 - API Basics
   1. API
     - Controllers
        - Product.cs : Controller
   1. Infrastructure
     - DbContext
        - StoreContext.cs : DbContext
            `dotnet ef migrations add InitialCreate`
            `dotnet ef database update`
   2. Core
     - Entities
       - Product.cs


# Section 3 - API Architecture
  1. Repository pattern:
     - Inject IRepository into the Controller: `GetProducts()`
     - Repository has access to DbContext: `_context.Product.ToList()`
     - DbCOntext will translate into a SQL query: `SELECT * FROM PRODUCTS`
  2. Steps for Repo Pattern:
     - Add Interface: `/Core/Entities/IProductRepository.cs`
     - Implement it : `/Infrastructure/Data/ProductRepository.cs`
        - Inject StoreContext: `ProductRepository(StoreContext _context)`
     - `/API/Startup.cs`: Register Service:
        - `services.AddScoped<IProductRepository, ProductRepository>()`
     - `/API/Controllers/Product.cs`:
        - Inject IProductRepository: `Product(IProductRepository _repository)`
   3. Add more entities: `/Core/Entities/ProductBrand.cs`, `/Core/Entities/ProductType.cs`
      - Migrations:
        - Drop existing tables: `dotnet ef database drop -p .\Infrastructure\ -s .\API\`
        - Remove old migrations: `dotnet ef migrations remove -p .\Infrastructure\ -s .\API\`
        - Migrate again: `dotnet ef migrations add InitialCreate -p .\Infrastructure\ -s .\API\ -o .\Infrastructure\Data\Migrations`
   4. Configure Migrations.
      - Add `/Infrastructure/Config/ProductConfigurations.cs`
      - override `OnModelCreating`
      - This time - another way to do migrations:
        - `/API/Program.cs`: `await context.Database.MigrateAsync();`
        - This will do pending migrations itself while building application.
   5. Eager loadig.
      - Eager loading is achieved using the Include() method.
      - `/API/Controllers/Products.cs`
   6. 