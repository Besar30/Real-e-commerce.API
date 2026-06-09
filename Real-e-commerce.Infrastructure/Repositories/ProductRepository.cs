using Microsoft.EntityFrameworkCore;
using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Infrastructure.Data;

namespace Real_e_commerce.Infrastructure.Repositories
{
    public class ProductRepository:Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public Task<bool> ProductExits(int id)
        {
            return _context.Products.AnyAsync(x=>x.Id== id);
        }
    }
}
