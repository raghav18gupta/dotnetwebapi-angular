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
  1. **Repository pattern**:
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
   5. **Eager loadig**.
      - Eager loading is achieved using the Include() method.
      - `/API/Controllers/Products.cs`

# Section 4 - API Generic Repository
   1. **Generic Repository**
      - `/Core/Interfaces/IGenericRepository.cs`
        - `where T : BaseEntity` - This repository will be used only by the classes which derives the `BaseEntity`.
      - `/Infrastructure/Data/GenericRepository.cs`
      - Register service - `/API/Startup.cs`
        - `services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));`
      - Issues with this design.
        - Need to inject multiple repositories.
        - Eagar loading not possible.
        - Generic Repository is an anti pattern!
   2. **Specification Pattern**
      - What is?
        - Describes a query in an object.
        - Returns an IQueryable<T>
        - Generic List method takes specification as a parameter.
        - Specification parameter can have a meaningful name.
      - How to?
        - `/Core/Specifications/ISpecification.cs` - Here we specify two properties: `Criteria` and `Includes`
        - `/Core/Specifications/BaseSpecification.cs` - Implements `ISpecification`. Additionally, has `AddInclude()` method.
        - `IGenericRepository` will have `GetEntityWithSpecification` and `ListAsync(ISpecification<T>)`
        - `/Infrastructure/Data/SpecificationEvaluator.cs` - will add includes in our query.
   3. **Shaping Data**
      - `/API/Dtos/ProductToReturnDto.cs`
   4. **AutoMapper**
      - `services.AddAutoMapper(typeof(MappingProfiles));`
      - `_mapper.Map<Product, ProductToReturnDto>(product)`
   5. Serve **static content from API**
      - Add new folder `/API/wwwroot`
      - `app.UseStaticFiles();`

# Section 5 - API Error Handling
   1. Check [delta](https://github.com/raghav18gupta/dotnetwebapi-angular/commit/f193c08e33c81d2a4585fb0ebb2d2902b71433db)

# Section 6 - API Paging, Filtering, Sorting, Searching
   1. **Sorting**
   2. **Filtering**
   3. **Searching**
   4. **Paging**
   5. **CORS**
# Section 9 - Client
   1. Modules
      - **App Module**: Autot generated, responsible for loading and bootstraping application. Making sure our components are loaded.
      - **Core Module**: Will have Singleton components, like - Navbar.
      - **Shared Module**: Anything we need to use in more than 1 feature module.
      - **Feature Module**: Each feature we create will have its own module and routing.
   2. commands
      - module: `ng g m core`
      - cd shop. component: `ng g c shop --flat --skip-tests`
      - service: `ng g s shop --flat --skip-tests`
      - module: `ng g m shop-routing --flat`
# Section 10: Routing
   1. **Lazy Loading**
      - `app\app-routing.module.ts`:
        - `{path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)},`

