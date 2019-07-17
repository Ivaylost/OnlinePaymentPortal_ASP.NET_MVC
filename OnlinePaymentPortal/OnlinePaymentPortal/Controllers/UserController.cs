using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Mappers;
using OnlinePaymentPortal.Models;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;

namespace OnlinePaymentPortal.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private readonly IUserService userServices;
        private readonly IAccountService accountServices;
        private readonly IViewModelMapper<IReadOnlyCollection<AccountDTO>, AccountDashViewModel> accountMapper;
        private readonly IViewModelMapper<IReadOnlyCollection<TransactionDTO>, TransactionsDashViewModel> transactionsMapper;

        public UserController(
            IUserService userServices,
            IAccountService accountServices,
            IViewModelMapper<IReadOnlyCollection<AccountDTO>, AccountDashViewModel> accountDash,
            IViewModelMapper<IReadOnlyCollection<TransactionDTO>, TransactionsDashViewModel> transactionsMapper)
        {
            this.userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
            this.accountServices = accountServices ?? throw new ArgumentNullException(nameof(accountServices));
            this.accountMapper = accountDash ?? throw new ArgumentNullException(nameof(accountDash));
            this.transactionsMapper = transactionsMapper ?? throw new ArgumentNullException(nameof(transactionsMapper));
        }

        public async Task<IActionResult> Accounts()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await this.userServices.GetUserAsync(userId);

            var userAccounts = await this.userServices.GetUserAccountsAsync(user.Id);

            var model = this.accountMapper.MapFrom(userAccounts.Accounts);
            return View(model);
        }

        public async Task<IActionResult> TransactionsForAccount(Guid Id)
        {
            var userTransactions = await this.userServices.GetAccountTransactionsAsync(Id);

            var model = this.transactionsMapper.MapFrom(userTransactions.Transactions);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditNickName(Guid Id)
        {
            var account = await this.userServices.GetAccountAsync(Id);
            var model = new AccountViewModel
            {
                Id = account.Id,
                NickName = account.Nickname,
            };


            return PartialView("_EditNickName", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNickName(AccountViewModel model)
        {
            try
            {
                var account = await this.userServices.GetAccountAsync(model.Id);
                var newAccount = new Account
                {
                    Id = account.Id,
                    AccountNumber = account.AccountNumber,
                    Nickname = model.NickName,
                    UsersAccounts = account.UsersAccounts,
                    ClientId = account.ClientId,
                    Balances = account.Balances,
                    SenderTransactions = account.SenderTransactions,
                    RecieverTransaction = account.RecieverTransaction
                };

                var updatedAccount = await this.accountServices.UpdateAccountAsync(account, newAccount);
                return RedirectToAction("Accounts", "User");
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MakePayment(Guid Id)
        {

            //var client = await this.accountServices.GetClientAsync(Id);
            var account = await this.accountServices.GetAccountByIdAsync(Id);
            var model = new TransactionsViewModel
            {
                PayerAccount = account.AccountNumber,
                PayerAccountId = account.Id,
                PayerClientName = account.ClientName
            };
            return PartialView("_MakePayment", model);
        }

        public JsonResult GetAccounts(string searchItem)
        {
            var dataList = this.accountServices.GetAllAccounts(searchItem);
            var modifiedData = dataList.Select(x => new
            {
                id = x.Id,
                text = x.AccountNumber
            });
            return Json(modifiedData);
        }
        [HttpPost]
        public async Task<IActionResult> MakePayment(TransactionsViewModel model)
        {
            var transaction =
                await this.userServices.AddTransactionAsync(model.PayerAccountId, model.PayeeAccountId, model.PaymentDescription, model.Ammount);


            return RedirectToAction("AllSavedTransactions", "User");
        }

        [HttpGet]
        public IActionResult MakeFullPayment()
        {
            return PartialView("_MakeFullPayment");
        }

        public JsonResult GetAccountsForUser(string searchItem)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var dataList = this.accountServices.GetAllAccountsForUser(searchItem, userId);

            var modifiedData = dataList.Select(x => new
            {
                id = x.Id,
                text = x.AccountNumber
            });
            return Json(modifiedData);
        }

        [HttpPost]
        public async Task<IActionResult> MakeFullPayment(TransactionsViewModel model)
        {
            var transaction =await this.userServices.AddTransactionAsync(model.PayerAccountId, model.PayeeAccountId, model.PaymentDescription, model.Ammount);


            return RedirectToAction("AllSavedTransactions", "User");
        }

        public async Task<IActionResult> AllSavedTransactions(int currentPage = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var accountIds = this.accountServices.GetUserAccountsIds(userId);

            var transactions = await this.accountServices.GetAllUserTransactionsAsync(accountIds,currentPage);

            var model = this.transactionsMapper.MapFrom(transactions.Transactions);

            var totalpages = await this.accountServices.GetPageCount(accountIds);

            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.TotalPages = totalpages;

            return View("AllSavedTransactions", model);

        }

        public async Task<IActionResult> AllCompletedTransactions(int currentPage = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var accountIds = this.accountServices.GetUserAccountsIds(userId);

            var transactions = await this.accountServices.GetAllUserCompletedTransactionsAsync(accountIds, currentPage);

            var model = this.transactionsMapper.MapFrom(transactions.Transactions);

            var totalpages = await this.accountServices.GetPageCountForCompleteTransactions(accountIds);

            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.TotalPages = totalpages;

            return View("AllCompletedTransactions", model);

        }

        public async Task<IActionResult> GetClientName(Guid accountId)
        {
            var account = this.accountServices.GetAccountById(accountId);
            var name = account.Client.Name;
            return Json(new { message = name });
        }

        [HttpGet]
        public IActionResult CompleteTransaction(Guid Id)
        {
            var model = new TransactionsViewModel
            {
                Id = Id
            };

            return PartialView("_CompleteTransaction", model);
        }
        [HttpPost]
        public IActionResult CompleteTransaction(TransactionsViewModel model)
        {
            var transactionDTO = this.userServices.CompleteTransactionAsync(model.Id);

            return RedirectToAction("Accounts", "User");
        }


        [HttpGet]
        public IActionResult EditTransaction(Guid Id)
        {
            var transaction = this.accountServices.GetTransactionById(Id);
            var model = new TransactionsViewModel
            {
                Id = Id,
                PayerAccount=transaction.SenderAccount.AccountNumber,
                PayerAccountId=transaction.SenderAccountId,
                PayerClientName=transaction.SenderAccount.Client.Name,
                PayeeAccount=transaction.RecieverAccount.AccountNumber,
                PayeeClientName=transaction.RecieverAccount.Client.Name,
                PayeeAccountId=transaction.RecieverAccountId,
                Ammount=transaction.Amount,
                PaymentDescription=transaction.Description
            };

            return PartialView("_EditTransaction", model);
        }


        [HttpPost]
        public IActionResult EditTransaction(TransactionsViewModel model)
        {
            var transaction = this.accountServices.UpdateTransaction(model.Id, model.PayerAccountId, model.PayeeAccountId, model.PaymentDescription, model.Ammount);
            
            return RedirectToAction("AllSavedTransactions", "User");
        }
    }
}