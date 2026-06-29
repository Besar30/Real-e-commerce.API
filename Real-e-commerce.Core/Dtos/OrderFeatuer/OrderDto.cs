using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Dtos.OrderFeatuer
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public required string BuyerEmail { get; set; }
        public required ShippingAdress ShippingAdress { get; set; }
        public decimal ShippingPrice { get; set; }
        public required DeliveryMethod DeliveryMethod { get; set; } 
        public required PaymentSummery PaymentSummery { get; set; } 
        public List<OrderItemDto> OrderItems { get; set; } = [];
        public decimal SubTotal { get; set; }
        public required string Status { get; set; } 
        public decimal Total { get; set; }
        public required string PaymentIntentId { get; set; }
    }
}
