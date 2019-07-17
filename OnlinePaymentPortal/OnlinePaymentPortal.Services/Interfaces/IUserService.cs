using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> FindUserAsync(string username, string password);
        Task<UserDTO> RegisterUserAsync(string name, string userName, string password);
        Task<UsersAccounts> AddAccountAsync(Guid userId, Guid accountId);
        Task<CollectionsDTO> GetAllUsersAsync(int currentPage);
        Task<int> GetPageCount();
        Task<UserDTO> GetUserAsync(string userId);
        Task<CollectionsDTO> GetUserAccountsAsync(Guid userId);
        //TODO:
        Task<Account> GetAccountAsync(Guid Id);
        Task<CollectionsDTO> GetAccountTransactionsAsync(Guid accountId);
        Task<BalanceDTO> ChangeBalance(Guid Id,decimal balanceValue);

        Task<Transaction> AddTransactionAsync(Guid senderAccountId, Guid recieverAccountId, string description, decimal ammount);
        TransactionDTO CompleteTransactionAsync(Guid transactiodId);
    }
}
