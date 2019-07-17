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
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly ICurrencyService currencyService;
        private readonly IClientService clientService;
        private readonly IViewModelMapper<IReadOnlyCollection<AccountDTO>, AdminViewModel> accountViewModelMapper;

        public AccountController(IAccountService accountService,
            ICurrencyService currencyService,
            IClientService clientService,
            IViewModelMapper<IReadOnlyCollection<AccountDTO>, AdminViewModel> accountViewModelMapper)
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            this.accountViewModelMapper = accountViewModelMapper ?? throw new ArgumentNullException(nameof(accountViewModelMapper));
        }
      
        public async Task<IActionResult> AllAccounts(int currentPage = 1)
        {
            var accounts = await this.accountService.GetAllUsersAsync(currentPage);

            var model = this.accountViewModelMapper.MapFrom(accounts.Accounts);
            var totalpages = await this.accountService.GetPageCount();

            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.TotalPages = totalpages;

            return View("AllAccounts", model);
        }

      
    }
}