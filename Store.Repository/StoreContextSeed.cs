using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System.Text.Json;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
			try
			{
                if (context.productBrands != null && !context.productBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Store.Repository/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null)
                        await context.productBrands.AddRangeAsync(brands);
                }

                if (context.productTypes != null && !context.productTypes.Any())
                {
                    var typesData = File.ReadAllText("../Store.Repository/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null)
                        await context.productTypes.AddRangeAsync(types);
                }

                if (context.products != null && !context.products.Any())
                {
                    var productsData = File.ReadAllText("../Store.Repository/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null)
                        await context.products.AddRangeAsync(products);
                }

                if (context.deliveryMethods != null && !context.deliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Store.Repository/SeedData/delivery.json");

                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    if (deliveryMethods is not null)
                        await context.deliveryMethods.AddRangeAsync(deliveryMethods);
                }

                await context.SaveChangesAsync();
            }
			catch (Exception ex)
			{
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();

                logger.LogError(ex.Message);

            }
        }
    }
}
