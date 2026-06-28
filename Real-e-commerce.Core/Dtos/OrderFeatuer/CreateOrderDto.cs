using Real_e_commerce.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Dtos.OrderFeatuer
{
    public class CreateOrderDto
    {
        [Required]
        public string CartId { get; set; }
        [Required]
        public int DeliveryMethidId { get; set; }
        [Required]
        public ShippingAdress shippingAdress { get; set; }
        [Required]
        public PaymentSummery paymentSummery { get; set; }
    }
}
