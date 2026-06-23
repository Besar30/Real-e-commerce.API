using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_e_commerce.API.Extentions;
using Real_e_commerce.Core.Dtos.AddressFeatuer;
using Real_e_commerce.Core.Dtos.AuthenticationFeatuer;
using Real_e_commerce.Core.Entities;
using System.Security.Claims;

namespace Real_e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(SignInManager<AppUser> signInManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new AppUser()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email
            };
            var result=await signInManager.UserManager.CreateAsync(user,dto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            return Ok();     
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task <IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }
        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            if (User.Identity.IsAuthenticated == false) return NoContent();
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);
            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                address=user.Address.ToDto()
            });
        }
        [HttpGet]
        public  IActionResult GetAuthState()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false
            });
        }
        [Authorize]
        [HttpPost("address")]
        public async Task<IActionResult> CreateOrUpdateAddress([FromBody] AddressDto dto)
        {
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);
            if (user.Address == null)
            {
                user.Address = dto.ToEntity();
            }
            else
            {
                user.Address.UpdateFromDto(dto);
            }
            var result = await signInManager.UserManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest("Problem updating user address");
            return Ok(user.Address.ToDto());
        }
    }
}
