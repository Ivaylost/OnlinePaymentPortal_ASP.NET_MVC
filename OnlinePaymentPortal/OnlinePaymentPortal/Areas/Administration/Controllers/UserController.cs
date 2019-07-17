using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces;
using OnlinePaymentPortal.Areas.Administration.Models;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;
namespace OnlinePaymentPortal.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IViewModelMapper<IReadOnlyCollection<UserDTO>, AdminViewModel> userViewModelMapper;
        private readonly IViewModelMapper<IReadOnlyCollection<AccountDTO>, AdminViewModel> accountViewModelMapper;
        private readonly IViewModelMapper<AccountDTO, AccountViewModel> accountMapper;

        public UserController(IUserService userService,
            IAccountService accountService,
            IViewModelMapper<IReadOnlyCollection<UserDTO>, AdminViewModel> userViewModelMapper,
            IViewModelMapper<IReadOnlyCollection<AccountDTO>, AdminViewModel> accountViewModelMapper,
            IViewModelMapper<AccountDTO, AccountViewModel> accountMapper)
        {
           

            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.userViewModelMapper = userViewModelMapper ?? throw new ArgumentNullException(nameof(userViewModelMapper));
            this.accountViewModelMapper = accountViewModelMapper;
            this.accountMapper = accountMapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return PartialView("_RegisterUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Index", "ErrorHandler");
            }
            var user =await this.userService.RegisterUserAsync(model.Name, model.UserName,
                model.Password);

            return RedirectToAction("AllUsers", "User");
        }


        public async Task<IActionResult> AllUsers(int currentPage = 1)
        {
            var users = await this.userService.GetAllUsersAsync(currentPage);

            var model = this.userViewModelMapper.MapFrom(users.Users);
            var totalpages = await this.userService.GetPageCount();

            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.TotalPages = totalpages;

            return View("AllUsers", model);
        }

        [HttpGet]
        public async Task<IActionResult> AddAccounts(Guid Id)
        {
            var accounts =await this.accountService.GetAllAccountsAsync();

            var model = new AddAccountsViewModel
            {
                UserId = Id,
                Accounts= accounts.Accounts.Select(x => new SelectListItem(x.AccountNumber, x.Id.ToString()))
            };
            return PartialView("_AddAccounts",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccounts(AddAccountsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Index", "ErrorHandler");
            }

            var account =await this.accountService.GetAccountByIdAsync(model.AccountId);

            var userAccount =await this.userService.AddAccountAsync(model.UserId,account.Id);

            return RedirectToAction("AllUsers", "User");

        }

        public async Task<IActionResult> UserAccounts(Guid Id)
        {
            var userAccounts = await this.userService.GetUserAccountsAsync(Id);

            var model = this.accountViewModelMapper.MapFrom(userAccounts.Accounts);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeBalance(Guid Id)
        {
            var account = await this.accountService.GetAccountByIdAsync(Id);

            var model = new AccountViewModel
            {
                Id = Id,
                BalanceValue=account.BalanceValue
            };
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return PartialView("_ChangeBalance",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeBalance(AccountViewModel model, string returnUrl)
        {
            var balance =await this.userService.ChangeBalance(model.Id,model.BalanceValue);

            return Redirect(returnUrl);
        }

    }
}