using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;

namespace Real_e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController (ICartServices cartServices): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCartById(string id)
        {
            var cart= await cartServices.GetCartAsync(id);
            return Ok(cart ?? new ShoppingCart { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(ShoppingCart cart)
        {
            var updateCart=await cartServices.SetCartAsync(cart);
            if (updateCart == null) return BadRequest("Problem with cart");
            return Ok(updateCart);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var result=await cartServices.DeleteCartASync(id);
            return result == false ? BadRequest("problem deleteing cart") : Ok();
        }
    }
}
