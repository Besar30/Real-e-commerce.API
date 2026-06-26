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
    public class DeliveryMethodRepository: Repository<DeliveryMethod>,IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(ApplicationDbContext dbContext):base(dbContext) {

        }
    }
}
