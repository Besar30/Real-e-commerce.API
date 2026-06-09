using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_e_commerce.Core.Dtos.ProductFeatuer;
using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;

namespace Real_e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products =await unitOfWork.ProductRepository.GetAll().ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
           Product product=await unitOfWork.ProductRepository.GetById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            unitOfWork.ProductRepository.Add(product);
            bool result=await unitOfWork.Save();
            if(result) 
                return Created();
            return BadRequest();
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute]int id,[FromBody]UpdateProductDto prod)
        {
            Product product = await unitOfWork.ProductRepository.GetById(id);
            if (product == null)
                return NotFound();
           prod.UpdateEntity(product);
            bool result = await unitOfWork.Save();
            if (result)
                return Ok(product);
            return Ok("No changes were made");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product product = await unitOfWork.ProductRepository.GetById(id);
            if (product == null)
                return NotFound();
            unitOfWork.ProductRepository.DeleteById(product);
            await unitOfWork.Save();
            return Ok();
        }
    }
}
