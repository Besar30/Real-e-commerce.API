using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_e_commerce.API.Extentions;
using Real_e_commerce.API.RequestHelpers;
using Real_e_commerce.Core.Dtos.OrderFeatuer;
using Real_e_commerce.Core.Entities.OrderAggregate;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Core.Specifiactions;

namespace Real_e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController(IUnitOfWork unitOfWork,IPaymentServices paymentServices) : ControllerBase
    {
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders([FromQuery]OrderSpecParsms specPrams)
        {
            var spec=new OrderSpecification(specPrams);
            var result= await unitOfWork.OrderRepositoty.ListAsync(spec);
            var ordersDto = result
                                .Select(o => o.ToDto())
                                .ToList();
            var count=await unitOfWork.OrderRepositoty.CountAsync(spec);
            var paginationResult=  PaginationResult<OrderDto>.CreatePagination(ordersDto, count,specPrams.pageIndex,specPrams.PageSize);
            return Ok(paginationResult);
        }
        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var spec = new OrderSpecification(id);
            var order=await unitOfWork.OrderRepositoty.GetEntityWithSpec(spec);
            if (order == null) return BadRequest("No order with that id");
            return Ok(order.ToDto());
        }
        [HttpPost("orders/refund/{id}")]
        public async Task<IActionResult> RefundOrder(int id)
        {
            var spec= new OrderSpecification(id);
            var order = await unitOfWork.OrderRepositoty.GetEntityWithSpec(spec);
            if (order == null) return BadRequest("No order with that id");
            if (order.Status == OrderStatus.Pending)
                return BadRequest("Payment Not received for this order");
            var result = await paymentServices.RefundPayment(order.PaymentIntentId);
            if (result == "succeeded")
            {
                order.Status = OrderStatus.Refunded;
                await unitOfWork.Save();
                return Ok( order.ToDto());
            }
            return BadRequest();
        }
    }
}
