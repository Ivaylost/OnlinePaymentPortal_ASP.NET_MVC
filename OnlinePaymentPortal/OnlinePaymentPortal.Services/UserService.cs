using Microsoft.EntityFrameworkCore;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly IHashService hashService;
        private readonly IDtoMapper<IReadOnlyCollection<Account>, CollectionsDTO> allAccountsDTOMapper;
        private readonly IDtoMapper<IReadOnlyCollection<User>, CollectionsDTO> allUsersDTOMapper;
        private readonly IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO> alltransactionsDTOMapper;
        private readonly IDtoMapper<User, UserDTO> userDTOMapper;
        private readonly IDtoMapper<Transaction, TransactionDTO> transactionDTOMapper;
        private readonly IDtoMapper<Balance, BalanceDTO> balanceDTOMapper;

        public UserService(ApplicationDbContext context,
            IHashService hashService,
            IDtoMapper<IReadOnlyCollection<Account>, CollectionsDTO> allAccountsDTOMapper,
            IDtoMapper<IReadOnlyCollection<User>, CollectionsDTO> allUsersDTOMapper,
            IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO> alltransactionsDTOMapper,
            IDtoMapper<User, UserDTO> userDTOMapper,
            IDtoMapper<Transaction, TransactionDTO> transactionDTOMapper,
            IDtoMapper<Balance, BalanceDTO> balanceDTOMapper)
        {
            this.context = context;
            this.hashService = hashService;
            this.allAccountsDTOMapper = allAccountsDTOMapper;
            this.allUsersDTOMapper = allUsersDTOMapper;
            this.alltransactionsDTOMapper = alltransactionsDTOMapper;
            this.userDTOMapper = userDTOMapper;
            this.transactionDTOMapper = transactionDTOMapper;
            this.balanceDTOMapper = balanceDTOMapper;
        }


        public async Task<CollectionsDTO> GetAllUsersAsync(int currentPage)
        {
            List<User> users;

            if (currentPage == 1)
            {
                users = await this.context
                    .Users
                    .OrderByDescending(c => c.CreatedOn)
                    .Take(5)
                    .ToListAsync();

            }
            else
            {
                users = await this.context
                    .Users
                    .OrderByDescending(c => c.CreatedOn)
                    .Skip((currentPage - 1) * 5)
                    .Take(5)
                    .ToListAsync();
            }

            return this.allUsersDTOMapper.MapFrom(users);
        }

        public async Task<int> GetPageCount()
        {
            var count = await this.context.Users.CountAsync();
            int pages = 0;

            if (count % 5 == 0)
            {
                pages = (count / 5);
            }
            else
            {
                pages = (count / 5) + 1;
            }
            return pages;
        }


        public async Task<UserDTO> RegisterUserAsync(string name, string userName, string password)
        {
            if (this.context.Users.Any(p => p.UserName == userName))
            {
                throw new ArgumentException($"User {userName} already exists");
            }

            var role = await this.context.Roles.FirstOrDefaultAsync(r => r.Name == "user");

            var salt = this.hashService.GenerateSalt();

            var hash = this.hashService.HashPassword(password, salt);

            var user = new User()
            {
                Name = name,
                UserName = userName,
                PasswordHash = hash,
                CreatedOn = DateTime.Now,
                Salt = "00123",
                RoleId = role.Id
            };

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            var userDTO=this.userDTOMapper.MapFrom(user);
            return userDTO;
        }

        public async Task<UserDTO> FindUserAsync(string username, string password)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            var hashedPassword = user.PasswordHash;

            if (!this.hashService.IsPasswordValid(password, hashedPassword))
            {
                return null;
            }

            var userDTO = this.userDTOMapper.MapFrom(user);
            return userDTO;
        }

        public async Task<UsersAccounts> AddAccountAsync(Guid userId, Guid accountId)
        {
            var userAccount = new UsersAccounts()
            {
                UserId = userId,
                AccountId = accountId
            };

            await this.context.UsersAccounts.AddAsync(userAccount);
            await this.context.SaveChangesAsync();

            return userAccount;
        }

        public async Task<CollectionsDTO> GetAccountTransactionsAsync(Guid accountId)
        {
            var transactions = await this.context.Transactions
                        .Where(p => p.SenderAccountId == accountId || p.RecieverAccountId == accountId)
                        .Include(x => x.SenderAccount)
                        .ThenInclude(x => x.Client)
                        .Include(x => x.RecieverAccount)
                        .ThenInclude(x => x.Client)
                        .Include(x => x.Type)
                        .OrderByDescending(t => t.Timestamp)
                        .ToListAsync();

            return this.alltransactionsDTOMapper.MapFrom(transactions);
        }
        public async Task<CollectionsDTO> GetUserAccountsAsync(Guid userId)
        {
            var accounts = await this.context.UsersAccounts
                        .Where(x => x.UserId == userId)
                        .Select(x => x.Account)
                        .Include(x => x.Balances)
                        .ThenInclude(x => x.Currency)
                        //.Include(x => x.ClientsAccounts)
                        .Include(x => x.Client)
                        .ToListAsync();

            return this.allAccountsDTOMapper.MapFrom(accounts);

        }

        public async Task<UserDTO> GetUserAsync(string userId)
        {
            var user= await this.context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            var userDTO = this.userDTOMapper.MapFrom(user);
            return userDTO;
        }

        //public async Task<IReadOnlyCollection<Account>> GetUserInfoAsync(IReadOnlyCollection<string> accounts, Guid Id)
        //{
        //    return await this.context.Accounts
        //                .Include(x => x.UsersAccounts)
        //                .ThenInclude(x => x.User)
        //                .Include(x => x.Balances)
        //                .ThenInclude(x => x.Currency)
        //                .Include(x => x.RecieverTransaction)
        //                .Include(x => x.SenderTransactions)
        //                .ThenInclude(x => x.Type)
        //                .ToListAsync();
        //}

        public async Task<Account> GetAccountAsync(Guid Id)
        {
            return await this.context.Accounts
                    .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<BalanceDTO> ChangeBalance(Guid Id, decimal balanceValue)
        {
            var balanceToUpdate = await this.context.Balances.FirstOrDefaultAsync(p => p.AccountId == Id);

            var updatedBalance = new Balance
            {
                Id = balanceToUpdate.Id,
                AccountId = balanceToUpdate.AccountId,
                CurrencyId = balanceToUpdate.CurrencyId,
                Value = balanceValue
            };

            context.Entry(balanceToUpdate).CurrentValues.SetValues(updatedBalance);

            await this.context.SaveChangesAsync();
            var balanceDTO=this.balanceDTOMapper.MapFrom(updatedBalance);
            return balanceDTO;
        }

        public async Task<Transaction> AddTransactionAsync(Guid senderAccountId, Guid recieverAccountId, string description, decimal ammount)
        {
            var type = this.context.TransactionTypes.FirstOrDefault(p => p.Name == "save");
            var transaction = new Transaction
            {
                SenderAccountId = senderAccountId,
                RecieverAccountId = recieverAccountId,
                Amount = ammount,
                Description = description,
                Timestamp = DateTime.Now,
                TypeId = type.Id
            };

            await this.context.Transactions.AddAsync(transaction);
            await this.context.SaveChangesAsync();

            return transaction;
        }

        public TransactionDTO CompleteTransactionAsync(Guid transactiodId)
        {
            var transaction = this.context.Transactions.FirstOrDefault(t => t.Id == transactiodId);

            var senderBalance = this.context.Balances
                .FirstOrDefault(b => b.AccountId == transaction.SenderAccountId);
            if (senderBalance.Value<transaction.Amount)
            {
                throw new ArgumentException("Not enough amount in balance");
            }
            senderBalance.Value = senderBalance.Value - transaction.Amount;

            this.context.Balances.Update(senderBalance);
            this.context.SaveChanges();

            var recieverBalance = this.context.Balances
                .FirstOrDefault(b => b.AccountId == transaction.RecieverAccountId);

            recieverBalance.Value = recieverBalance.Value + transaction.Amount;

            this.context.Balances.Update(recieverBalance);
            this.context.SaveChanges();

            var completeType = this.context.TransactionTypes.FirstOrDefault(t => t.Name == "complete");

            transaction.TypeId = completeType.Id;

            this.context.Transactions.Update(transaction);
            this.context.SaveChanges();
            var transactionDTO = this.transactionDTOMapper.MapFrom(transaction);
            return transactionDTO;
        }
    }
}
