using Core.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext _context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!_context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var item in brands)
                    {
                        _context.ProductBrands.Add(item);
                    }
                    await _context.SaveChangesAsync();

                }
                if (!_context.ProductTypes.Any())
                {
                    var brandTypeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var brandsTypes = JsonSerializer.Deserialize<List<ProductType>>(brandTypeData);

                    foreach (var item in brandsTypes)
                    {
                        _context.ProductTypes.Add(item);
                    }
                    await _context.SaveChangesAsync();

                }
                if (!_context.Products.Any())
                {
                    var productData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var item in products)
                    {
                        _context.Products.Add(item);
                    }
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
