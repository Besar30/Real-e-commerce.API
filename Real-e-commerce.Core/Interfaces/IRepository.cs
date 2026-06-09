using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task<T?> GetEntityWithSpec(ISpecifiaction<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecifiaction<T> spec);
        void Add(T entity);
        void Update(T entity);
        void DeleteById(T entity);
    }
}
