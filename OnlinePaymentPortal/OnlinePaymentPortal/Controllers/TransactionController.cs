using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces;
using OnlinePaymentPortal.Models;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;

namespace OnlinePaymentPortal.Controllers
{
    [Authorize(Roles = "user")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IAccountService accountService;
        private readonly IViewModelMapper<IReadOnlyCollection<TransactionDTO>, TransactionsDashViewModel> transactionsMapper;

        public TransactionController(ITransactionService transactionService,
            IAccountService accountService,
            IViewModelMapper<IReadOnlyCollection<TransactionDTO>, TransactionsDashViewModel> transactionsMapper)
        {
            this.transactionService = transactionService;
            this.accountService = accountService;
            this.transactionsMapper = transactionsMapper;
        }
        public async Task<IActionResult> AllTransactions()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var accountIds = this.accountService.GetUserAccountsIds(userId);

            var transactions = await this.transactionService.GetAllUserTransactionsAsync(accountIds);

            var model = this.transactionsMapper.MapFrom(transactions.Transactions);

            return View("AllTransactions", model);

        }

        public async Task<IActionResult> AllCompleteTransactions()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var accountIds = this.accountService.GetUserAccountsIds(userId);

            var transactions = await this.transactionService.GetAllUserTransactionsAsync(accountIds);

            var model = this.transactionsMapper.MapFrom(transactions.Transactions);

            return View("AllTransactions", model);

        }
    }
}