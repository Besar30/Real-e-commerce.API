using Real_e_commerce.Core.Entities.OrderAggregate;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Repositories
{
    public class OrderRepositoty:Repository<Order>, IOrderRepositoty
    {
        public OrderRepositoty(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
