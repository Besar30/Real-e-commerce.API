using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        Pending,
        PaymentRecived,
        PaymentFaild,
        PaymentMismatch
    }
}
