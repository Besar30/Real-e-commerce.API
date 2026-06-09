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
        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? Brand, string? Type,string? sort)
        {
            IQueryable<Product> products = _context.Products;
            if (!string.IsNullOrEmpty(Brand)) {
                products=products.Where(x=>x.Brand==Brand);
            }
            if (!string.IsNullOrEmpty(Type)) {
                products=products.Where(x=>x.Type==Type);
            }
            if (!string.IsNullOrEmpty(sort)) {
                products = sort switch
                {
                    "priceAsc" => products.OrderBy(x => x.Price),
                    "priceDesc"=>products.OrderByDescending(x => x.Price),
                    _=>products.OrderBy(x=>x.Name)
                };
            }
            return await products.ToListAsync();
        }
        public async Task<IReadOnlyList<string>> GetBrandsAsync()
        {
            return await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
        }

     

        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
            return await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
        }

        public Task<bool> ProductExits(int id)
        {
            return _context.Products.AnyAsync(x=>x.Id== id);
        }
    }
}
