using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> AddAccountAsync(Guid clientId, Guid currencyId);
        Task<CollectionsDTO> GetAllAccountsAsync();
        Task<AccountDTO> GetAccountByIdAsync(Guid Id);
        Task<CollectionsDTO> GetAllUsersAsync(int currentPage);
        Task<int> GetPageCount();
        Task<AccountDTO> UpdateAccountAsync(Account account, Account newAccount);
        List<Guid> GetUserAccountsIds(string userId);
        //Task<CollectionsDTO> GetAllUserTransactionsAsync(List<Guid> accountsIds);
        Task<CollectionsDTO> SearchAsync(string text);
        Task<CollectionsDTO> GetAllUserCompletedTransactionsAsync(List<Guid> accountsIds, int currentPage);
        ICollection<Account> GetAllAccounts(string searchItem);
        ICollection<Account> GetAllAccountsForUser(string searchItem, string userId);
        Account GetAccountById(Guid accountId);
        Transaction GetTransactionById(Guid transactionId);
        Transaction UpdateTransaction(Guid transactionId, Guid payerAccountId, Guid payeeAccountId, string description, decimal ammount);
        Task<CollectionsDTO> GetAllUserTransactionsAsync(List<Guid> accountsIds, int currentPage);
        Task<int> GetPageCount(List<Guid> accountsIds);
        Task<int> GetPageCountForCompleteTransactions(List<Guid> accountsIds);

    }
}
