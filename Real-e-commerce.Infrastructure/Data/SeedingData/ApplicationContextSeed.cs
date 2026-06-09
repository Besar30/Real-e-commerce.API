using Real_e_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Data.SeedingData
{
    public class ApplicationContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Real-e-commerce.Infrastructure/Data/SeedingData/Products.json");
                var products= JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
