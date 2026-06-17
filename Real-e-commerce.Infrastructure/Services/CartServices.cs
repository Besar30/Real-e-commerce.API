using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Services
{
    public class CartServices (IConnectionMultiplexer redis): ICartServices
    {
        private readonly IDatabase _database=redis.GetDatabase();
        public async Task<bool> DeleteCartASync(string Key)
        {
            return  await _database.KeyDeleteAsync(Key);
        }

        public async Task<ShoppingCart?> GetCartAsync(string Key)
        {
            var data=await _database.StringGetAsync(Key);
            return data.IsNullOrEmpty? null:JsonSerializer.Deserialize<ShoppingCart>(data!);
        }

        public async Task<ShoppingCart?> SetCartAsync(ShoppingCart Cart)
        {
            var created=await _database.StringSetAsync
                         (Cart.Id,JsonSerializer.Serialize(Cart),TimeSpan.FromDays(30));
            if(!created) return null;
            return await GetCartAsync(Cart.Id);
        }
    }
}
