using Real_e_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Interfaces
{
    public interface ICartServices
    {
        Task<ShoppingCart?> GetCartAsync(string Key);
        Task<ShoppingCart?> SetCartAsync(ShoppingCart Cart);
        Task<bool> DeleteCartASync(string Key);
    }
}
