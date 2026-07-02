using Microsoft.AspNetCore.Identity;
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
        public static async Task SeedAsync(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any(x => x.UserName == "admin@test.com"))
            {
                var user = new AppUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                };
                await userManager.CreateAsync(user,"asd123!A");
                await userManager.AddToRoleAsync(user, "Admin");
            }
            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Real-e-commerce.Infrastructure/Data/SeedingData/Products.json");
                var products= JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
            if (!context.DeliveryMethods.Any()) {
                var delivertData = await File.ReadAllTextAsync("../Real-e-commerce.Infrastructure/Data/SeedingData/delivery.json");
                var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(delivertData);
                context.DeliveryMethods.AddRange(deliveries);
                await context.SaveChangesAsync();
            }
        }
    }
}
