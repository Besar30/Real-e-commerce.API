using Microsoft.EntityFrameworkCore;
using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
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

        public async Task<T?> GetEntityWithSpec(ISpecifiaction<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecifiaction<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
       
        public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecifiaction<T, TResult> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecifiaction<T,TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecifiaction<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(dbset.AsQueryable(), spec);

        }
        private IQueryable<TResult> ApplySpecification<TResult>(ISpecifiaction<T,TResult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery<T,TResult>(dbset.AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecifiaction<T> Spec)
        {
           var query=dbset.AsQueryable();
            query = Spec.ApplyCriteria(query);
            return await query.CountAsync();
        }
    }
}
