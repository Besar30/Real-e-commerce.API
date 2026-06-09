using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        Task<bool> Save();
    }
}
