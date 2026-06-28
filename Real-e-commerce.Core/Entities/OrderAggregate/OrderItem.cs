using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Entities.OrderAggregate
{
    public class OrderItem:BaseEntity
    {
        public ProductItemOrder itemOrdered { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
