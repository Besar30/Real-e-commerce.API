using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_e_commerce.API.Extentions;
using Real_e_commerce.Core.Dtos.OrderFeatuer;
using Real_e_commerce.Core.Entities.OrderAggregate;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Core.Specifiactions;

namespace Real_e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController(IUnitOfWork unitOfWork,ICartServices cartServices) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            string Email = User.GetEmail();
            if (string.IsNullOrEmpty(Email)) return BadRequest("Email NotFound");
            var cart = await cartServices.GetCartAsync(dto.CartId);
            if (cart == null) return BadRequest("cart NOt Found");
            if (cart.PaymentIntentId == null) return BadRequest("No Payment intent for this order");
            var items= new List<OrderItem>();
            foreach (var item in cart.Items){
                var found = await unitOfWork.ProductRepository.GetById(item.ProductId);
                if (found == null) return BadRequest("Problem with Order");
                var ItemOrdered = new ProductItemOrder
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    PictureUrl = item.PictureUrl,
                };
                var orderItem = new OrderItem
                {
                    itemOrdered = ItemOrdered,
                    Price = item.Price,
                    Quantity = item.Quantity,
                };
                items.Add(orderItem);
            }
            var deliveryMethod = await unitOfWork.DeliveryMethodRepository.GetById(dto.DeliveryMethidId);
            if (deliveryMethod == null) return BadRequest("No Delivery method selected");
            var order = new Order
            {
                BuyerEmail = Email,
                PaymentSummery = dto.paymentSummery,
                DeliveryMethod = deliveryMethod,
                PaymentIntentId = cart.PaymentIntentId,
                ShippingAdress = dto.shippingAdress,
                OrderItems = items,
                SubTotal = items.Sum(x => x.Quantity * x.Price)
            };
            unitOfWork.OrderRepositoty.Add(order);
            if(await unitOfWork.Save())
                return Ok(order);
            return BadRequest("Problem Creating order");
        }
        [HttpGet]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var spec = new OrderSpecification(User.GetEmail());
            var orders = await unitOfWork.OrderRepositoty.ListAsync(spec);
            return Ok(orders);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var spec=new OrderSpecification(User.GetEmail(),id);
            var order=await unitOfWork.OrderRepositoty.GetEntityWithSpec(spec);
            if(order == null) return NotFound();    
            return Ok(order);
        }

    }
}
