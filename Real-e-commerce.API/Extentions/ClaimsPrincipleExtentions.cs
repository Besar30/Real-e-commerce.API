using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Real_e_commerce.Core.Entities;
using System.Security.Authentication;
using System.Security.Claims;

namespace Real_e_commerce.API.Extentions
{
    public static class ClaimsPrincipleExtentions
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.GetEmail());
            if (userToReturn == null) throw new AuthenticationException("User Not Found");
            return userToReturn;
        }
        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users.Include(x=>x.Address).FirstOrDefaultAsync(x => x.Email == user.GetEmail());
            if (userToReturn == null) throw new AuthenticationException("User Not Found");
            return userToReturn;
        }
        public static string  GetEmail(this ClaimsPrincipal user)
        {
            var email=user.FindFirstValue(ClaimTypes.Email);
            if (email == null) throw new AuthenticationException("Email claim Not Found");
            return email;
        }

        
    }
}
