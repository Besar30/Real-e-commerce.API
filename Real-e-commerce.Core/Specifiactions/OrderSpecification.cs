using Real_e_commerce.Core.Entities.OrderAggregate;
namespace Real_e_commerce.Core.Specifiactions
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string email):base(x=>x.BuyerEmail==email)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.OrderItems);
            AddOrderByDescending(x=>x.OrderDate);
        }
        public OrderSpecification(string email,int id):base(x=>x.BuyerEmail== email &&x.Id==id)
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }
        public OrderSpecification(string paymentIntentId,bool isPaymentIntent):base(x=>x.PaymentIntentId== paymentIntentId)
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }
        public OrderSpecification(OrderSpecParsms orderSpec):base(x=>
           string.IsNullOrEmpty(orderSpec.Status) || x.Status==ParseStatus(orderSpec.Status))
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
            ApplyPagination(orderSpec.PageSize * (orderSpec.pageIndex - 1), orderSpec.PageSize);
            AddOrderByDescending(x => x.OrderDate);
        }
        public OrderSpecification(int id):base(x=>x.Id==id) 
        {
            AddInclude("OrderItems");
            AddInclude("DeliveryMethod");
        }
        private static OrderStatus? ParseStatus(string status)
        {
            if(Enum.TryParse<OrderStatus>(status,true, out var statusValue))
            {
                return statusValue;
            }
            return null;
        }
    }
}
