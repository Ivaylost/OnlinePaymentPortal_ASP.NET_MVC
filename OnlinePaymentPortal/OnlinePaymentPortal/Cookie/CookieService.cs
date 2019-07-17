using Microsoft.AspNetCore.Authentication;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Cookie
{
    public class CookieService : ICookieService
    {
        public List<Claim> PrepareClaims(AdminDTO admin, RoleDTO role)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,admin.Id.ToString()),
                new Claim(ClaimTypes.Name,admin.UserName),
                new Claim(ClaimTypes.Role,role.Name),

            };
        }
        public List<Claim> PrepareClaims(UserDTO user, RoleDTO role)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,role.Name),

            };
        }

        public AuthenticationProperties GetCookieOptions()
        {
            return new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };
        }


    }
}
