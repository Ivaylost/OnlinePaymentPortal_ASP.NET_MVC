using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlinePaymentPortal.Areas.Administration.Models;
using OnlinePaymentPortal.Cookie;
using OnlinePaymentPortal.Services.Interfaces;


namespace OnlinePaymentPortal.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IRoleService roleService;
        private readonly ICookieService cookieService;

        public AdminController(IAdminService adminService,
            IRoleService roleService,
            ICookieService cookieService)
        {
            this.adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            this.roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            this.cookieService = cookieService ?? throw new ArgumentNullException(nameof(cookieService));
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 86400)]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AdminLoginViewModel loginModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", loginModel);
            }

            var admin = await this.adminService.FindAdminAsync(loginModel.UserName, loginModel.Password);

            var role = await this.roleService.GetRoleByIdAsync(admin.RoleId);

            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var indentity = new ClaimsIdentity(this.cookieService.PrepareClaims(admin, role), scheme);

            await this.HttpContext.SignInAsync(scheme, new ClaimsPrincipal(indentity), this.cookieService.GetCookieOptions());

            return RedirectToAction("AllUsers", "User");
        }

        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Admin");
        }
    }
}