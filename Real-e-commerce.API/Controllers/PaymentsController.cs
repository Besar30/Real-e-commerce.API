using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;

namespace Real_e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IPaymentServices paymentServices,IUnitOfWork unitOfWork): ControllerBase
    {
        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<IActionResult>CreateOrUpdatePaymentIntent([FromRoute]string cartId)
        {
            var cart=await paymentServices.CreateOrUpdatePaymentIntent(cartId);
            if (cart == null) return BadRequest("problem with your cart");
            return Ok(cart);
        }
        [HttpGet("delivery-methods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            return Ok(await unitOfWork.DeliveryMethodRepository.GetAll().ToListAsync());
        }

    }
}
