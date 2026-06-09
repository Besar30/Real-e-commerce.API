using Real_e_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<bool> ProductExits(int id);
        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypesAsync();
        Task<IReadOnlyList<Product>> GetProductsAsync(string? Brand,string? Type,string? sort);
    }
}
