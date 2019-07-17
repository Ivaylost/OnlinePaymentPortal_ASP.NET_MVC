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
    public class ClientController : Controller
    {
        private readonly IViewModelMapper<IReadOnlyCollection<ClientDTO>, AdminViewModel> clientViewModelMapper;
        private readonly IClientService clientService;
        private readonly ICurrencyService currencyService;
        private readonly IAccountService accountService;

        public ClientController(IViewModelMapper<IReadOnlyCollection<ClientDTO>, AdminViewModel> clientViewModelMapper,
            IClientService clientService,
            ICurrencyService currencyService,
            IAccountService accountService)
        {
            this.clientViewModelMapper = clientViewModelMapper ?? throw new ArgumentNullException(nameof(clientViewModelMapper));
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            this.currencyService = currencyService;
            this.accountService = accountService;
        }
        public async Task<IActionResult> AllClients(int currentPage = 1)
        {
            var clients = await this.clientService.GetAllClientsAsync(currentPage);
            var model = this.clientViewModelMapper.MapFrom(clients.Clients);

            var totalpages = await this.clientService.GetPageCount();

            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.TotalPages = totalpages;

            return View("AllClients", model);
        }

        [HttpGet]
        public IActionResult AddClientAsync()
        {
            return PartialView("_AddClient");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClientAsync(ClientViewModel model)
        {
            var isContains = this.clientService.IsContains(model.Name);

            if (!ModelState.IsValid || isContains == true)
            {
                return RedirectToAction("Index", "ErrorHandler");
            }

            var client = await this.clientService.CreateClientAsync(model.Name);

            return RedirectToAction("AllClients", "Client");
        }


        [HttpGet]
        public async Task<IActionResult> AddAccount(Guid id)
        {
            var currencies = await this.currencyService.GetAllCurrenciesAsync();

            var model = new OnlinePaymentPortal.Areas.Administration.Models.AccountViewModel
            {
                ClientId = id,
                Currencies = currencies.Currencies.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            };
            return PartialView("_AddAccount",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccount(AccountViewModel model)
        {

            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("Index", "ErrorHandler");
            //}

            var account = await this.accountService.AddAccountAsync(model.ClientId, model.CurrencyId);

            return RedirectToAction("AllClients", "Client");
        }
    }
}