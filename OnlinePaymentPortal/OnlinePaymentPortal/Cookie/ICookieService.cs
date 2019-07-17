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
    public interface ICookieService
    {
        List<Claim> PrepareClaims(UserDTO user, RoleDTO role);
        List<Claim> PrepareClaims(AdminDTO admin, RoleDTO role);
        AuthenticationProperties GetCookieOptions();
    }
}
