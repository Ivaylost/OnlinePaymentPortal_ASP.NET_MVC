using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OnlinePaymentPortal.Cookie;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Models;
using OnlinePaymentPortal.Services.Interfaces;

namespace OnlinePaymentPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly ICookieService cookieService;
        private readonly IBannerService bannerService;

        public HomeController(IUserService userService,
            IRoleService roleService,
            ICookieService cookieService,
            IBannerService bannerService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            this.cookieService = cookieService ?? throw new ArgumentNullException(nameof(cookieService));
            this.bannerService = bannerService ?? throw new ArgumentNullException(nameof(bannerService));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var banner = bannerService.GetBanner();
            if (banner == null)
            {
                return View();
            }
            ViewData["Banner"] = banner;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel loginModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", loginModel);
            }

            var user = await this.userService.FindUserAsync(loginModel.UserName, loginModel.Password);

            var banner = bannerService.GetBanner();
            ViewData["Banner"] = banner;

            if (user == null)
            {
                this.ModelState.AddModelError("key", "Invalid Logging Attempt");

                
                return View("Index", loginModel);
            }
            var role =await this.roleService.GetRoleByIdAsync(user.RoleId);

            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var indentity = new ClaimsIdentity(this.cookieService.PrepareClaims(user, role), scheme);

            await this.HttpContext.SignInAsync(scheme, new ClaimsPrincipal(indentity), this.cookieService.GetCookieOptions());

            return RedirectToAction("Accounts", "User", new { Id = user.Id });
        }

        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
