using Microsoft.EntityFrameworkCore;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> dbset;
        public Repository(ApplicationDbContext context)
        {
            _context=context;
            dbset = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return dbset.AsQueryable();
        }
        public async Task<T> GetById(int id)
        {
            return await dbset.FindAsync(id);
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }
        public void Update(T entity)
        {
            dbset.Update(entity);
        }
        public void DeleteById(T entity)
        {
           dbset.Remove(entity); 
        }
     
    }
}
