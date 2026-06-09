using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_e_commerce.Core.Dtos.ProductFeatuer;
using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Core.Specifiactions;

namespace Real_e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(string? Brand,string? Type,string? sort)
        {
            //var products =await unitOfWork.ProductRepository.GetProductsAsync(Brand,Type,sort);
            ISpecifiaction<Product> spac=new ProductSpecification(Brand,Type,sort);
            var products=await unitOfWork.ProductRepository.ListAsync(spac);
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
        [HttpGet("Brands")]
        public async Task<IActionResult> GetBrands()
        {
            var Brands=await unitOfWork.ProductRepository.GetBrandsAsync();
            return Ok(Brands);
        }
        [HttpGet("Types")]
        public async Task<IActionResult> GetTypes()
        {
            var Types = await unitOfWork.ProductRepository.GetTypesAsync();
            return Ok(Types);
        }
    }
}
