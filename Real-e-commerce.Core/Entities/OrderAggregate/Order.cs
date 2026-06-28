using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Entities.OrderAggregate
{
    public class Order:BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public required string BuyerEmail { get; set; }
        public ShippingAdress ShippingAdress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public PaymentSummery PaymentSummery { get; set; }=null!;
        public List<OrderItem> OrderItems { get; set; } = [];
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public required string PaymentIntentId { get; set; }
    }
}
