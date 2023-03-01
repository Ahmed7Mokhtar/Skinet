using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandData = await File.ReadAllTextAsync("../Infrastructure\\Data\\SeedData\\brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData) ?? new List<ProductBrand>();

                    foreach(var item in brands)
                    {
                        await context.ProductBrands.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync("../Infrastructure\\Data\\SeedData\\types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData) ?? new List<ProductType>();

                    foreach(var item in types)
                    {
                        await context.ProductTypes.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }
                
                if(!context.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync("../Infrastructure\\Data\\SeedData\\products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData) ?? new List<Product>();

                    foreach(var item in products)
                    {
                        await context.Products.AddAsync(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.DeliveryMethods.Any())
                {
                    var dmData = await File
                        .ReadAllTextAsync("../Infrastructure\\Data\\SeedData\\delivery.json");

                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach(var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            } catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}