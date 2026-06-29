using Real_e_commerce.Core.Dtos.OrderFeatuer;
using Real_e_commerce.Core.Entities.OrderAggregate;

namespace Real_e_commerce.API.Extentions
{
    public static class OrderMappingExtentions
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id= order.Id,
                BuyerEmail= order.BuyerEmail,
                OrderDate= order.OrderDate,
                ShippingAdress= order.ShippingAdress,
                ShippingPrice= order.DeliveryMethod.Price,
                PaymentIntentId= order.PaymentIntentId,
                PaymentSummery= order.PaymentSummery,
                OrderItems= order.OrderItems.Select(x=>x.ToDto()).ToList(),
                SubTotal= order.SubTotal,
                Status= order.Status.ToString(),
                DeliveryMethod=order.DeliveryMethod,
                Total=order.GetTotal(),
            };
        }
        public static OrderItemDto ToDto(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                ProductId = orderItem.itemOrdered.ProductId,
                ProductName = orderItem.itemOrdered.ProductName,
                PictureUrl = orderItem.itemOrdered.PictureUrl,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
            };
        }
    }
}
