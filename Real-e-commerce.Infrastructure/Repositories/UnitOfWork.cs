using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IProductRepository productRepository;
        private IDeliveryMethodRepository deliveryMethodRepository;
       
        public UnitOfWork(ApplicationDbContext context)
        {
            _context=context;
        }

        public IProductRepository ProductRepository {
            get => productRepository ??= new ProductRepository(_context);
        }
        public IDeliveryMethodRepository DeliveryMethodRepository
        {
            get => deliveryMethodRepository ??= new DeliveryMethodRepository(_context);
        }
        private IOrderRepositoty orderRepositoty;
        public IOrderRepositoty OrderRepositoty
        {
            get => orderRepositoty ?? new OrderRepositoty(_context);
        }

        public async Task<bool> Save()
        {
            var result= await _context.SaveChangesAsync();
            return result>0;
        }
    }
}
