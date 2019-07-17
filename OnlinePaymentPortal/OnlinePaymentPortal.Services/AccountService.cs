using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext context;
        private readonly IDtoMapper<Account, AccountDTO> accountDTOMapper;
        private readonly IDtoMapper<Client, ClientDTO> clinetDTOMapper;
        private readonly IDtoMapper<IReadOnlyCollection<Account>, CollectionsDTO> allAccountsDTOMapper;
        private readonly IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO> allTransactionsDTOMapper;
        private readonly IConfiguration config;

        public AccountService(ApplicationDbContext context,
            IDtoMapper<Account, AccountDTO> accountDTOMapper,
            IDtoMapper<Client, ClientDTO> clinetDTOMapper,
            IDtoMapper<IReadOnlyCollection<Account>, CollectionsDTO> allAccountsDTOMapper,
            IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO> allTransactionsDTOMapper,
            IConfiguration config)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.accountDTOMapper = accountDTOMapper ;
            this.clinetDTOMapper = clinetDTOMapper ;
            this.allAccountsDTOMapper = allAccountsDTOMapper ;
            this.allTransactionsDTOMapper = allTransactionsDTOMapper ;
            this.config = config;
        }

        public async Task<AccountDTO> GetAccountByIdAsync(Guid Id)
        {
            var account = await this.context.Accounts
                .Include(p => p.Balances)
                .Include(c => c.Client)
                .FirstOrDefaultAsync(a => a.Id == Id);
            var accountDTO = this.accountDTOMapper.MapFrom(account);
            return accountDTO;
        }

        public async Task<AccountDTO> AddAccountAsync(Guid clientId, Guid currencyId)
        {
            var random = new Random();
            string generator = string.Empty;
            for (int i = 0; i < 10; i++)
                generator = String.Concat(generator, random.Next(10).ToString());

            var account = new Account()
            {
                AccountNumber = generator,
                Nickname = generator,
                CreatedOn = DateTime.Now,
                ClientId = clientId,
                Client = this.context.Clients.FirstOrDefault(c => c.Id == clientId)
            };

            await this.context.Accounts.AddAsync(account);

            var configBalance = decimal.Parse(config.GetSection("GlobalConstants:Balance").Value);
            var balance = new Balance
            {
                Value = configBalance,
                CurrencyId = currencyId,
                AccountId = account.Id
            };


            await this.context.Balances.AddAsync(balance);

            await this.context.SaveChangesAsync();


            var accountDTO = this.accountDTOMapper.MapFrom(account);
            return accountDTO;

        }


        public async Task<CollectionsDTO> GetAllAccountsAsync()
        {
            var accounts = await this.context.Accounts
                .Include(x => x.Balances)
                .ThenInclude(c => c.Currency)
                .Include(c => c.Client)
                .OrderByDescending(t=>t.CreatedOn)
                .ToListAsync();

            var accountsDTO = this.allAccountsDTOMapper.MapFrom(accounts);

            return accountsDTO;
        }

        public async Task<CollectionsDTO> GetAllUsersAsync(int currentPage)
        {
            List<Account> accounts;

            if (currentPage == 1)
            {
                accounts = await this.context
                    .Accounts
                    .Include(x => x.Balances)
                    .ThenInclude(c => c.Currency)
                    .Include(c => c.Client)
                    .OrderByDescending(c => c.CreatedOn)
                    .Take(5)
                    .ToListAsync();

            }
            else
            {
                accounts = await this.context
                    .Accounts
                    .Include(x => x.Balances)
                    .ThenInclude(c => c.Currency)
                    .Include(c => c.Client)
                    .OrderByDescending(c => c.CreatedOn)
                    .Skip((currentPage - 1) * 5)
                    .Take(5)
                    .ToListAsync();
            }

            return this.allAccountsDTOMapper.MapFrom(accounts);
        }

        public async Task<int> GetPageCount()
        {
            var count = await this.context.Accounts.CountAsync();
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

        public async Task<AccountDTO> UpdateAccountAsync(Account account, Account newAccount)
        {
            context.Entry(account).CurrentValues.SetValues(newAccount);
            await this.context.SaveChangesAsync();
            var accountmap = this.context.Accounts.Include(p => p.Balances).Include(c => c.Client).FirstOrDefault(p => p.Id == newAccount.Id);
            var accountDTO = this.accountDTOMapper.MapFrom(accountmap);
            return accountDTO;
        }


        public List<Guid> GetUserAccountsIds(string userId)
        {
            var allUsersAccountsForUser = this.context.UsersAccounts
                .Include(a => a.Account)
                .Where(x => x.UserId.ToString() == userId)
                .Select(x => x.AccountId)
                .ToList();

            return allUsersAccountsForUser;
        }

        public async Task<CollectionsDTO> GetAllUserTransactionsAsync(List<Guid> accountsIds, int currentPage)
        {
            List<Transaction> TransactOfAllUserAccounts;

            if (currentPage == 1)
            {
                TransactOfAllUserAccounts = await this.context.Transactions
                                                            .Include(x => x.Type)
                                                            .Include(x => x.SenderAccount)
                                                            .ThenInclude(x => x.Client)
                                                            .Include(x => x.RecieverAccount)
                                                            .ThenInclude(x => x.Client)
                                                            .Where(t => accountsIds
                                                            .Contains(t.SenderAccountId))
                                                            .Where(p => p.Type.Name == "save")
                                                            .OrderByDescending(t=>t.Timestamp)
                                                            .Take(5)
                                                            .ToListAsync();

            }
            else
            {
                TransactOfAllUserAccounts = await this.context.Transactions
                                                             .Include(x => x.Type)
                                                             .Include(x => x.SenderAccount)
                                                             .ThenInclude(x => x.Client)
                                                             .Include(x => x.RecieverAccount)
                                                             .ThenInclude(x => x.Client)
                                                             .Where(t => accountsIds
                                                             .Contains(t.SenderAccountId))
                                                             .Where(p => p.Type.Name == "save")
                                                             .OrderByDescending(t => t.Timestamp)
                                                             .Skip((currentPage - 1) * 5)
                                                             .Take(5)
                                                             .ToListAsync();
            }

            var transactionsDTO = this.allTransactionsDTOMapper.MapFrom(TransactOfAllUserAccounts);

            return transactionsDTO;
        }

        public async Task<int> GetPageCount(List<Guid> accountsIds)
        {
            var count = await this.context.Transactions
                                          .Include(x => x.Type)
                                          .Include(x => x.SenderAccount)
                                          .ThenInclude(x => x.Client)
                                          .Include(x => x.RecieverAccount)
                                          .ThenInclude(x => x.Client)
                                          .Where(t => accountsIds
                                          .Contains(t.RecieverAccountId)
                                          || accountsIds
                                          .Contains(t.SenderAccountId))
                                          .Where(p => p.Type.Name == "save")
                                          .CountAsync();
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


        public async Task<CollectionsDTO> SearchAsync(string text)
        {
            var accounts = await this.context.Accounts
                .Include(x => x.Client)
                .Where(x => x.AccountNumber.Contains(text))
                .ToListAsync();

            var accountsDTO = this.allAccountsDTOMapper.MapFrom(accounts);

            return accountsDTO;
        }

        public async Task<CollectionsDTO> GetAllUserCompletedTransactionsAsync(List<Guid> accountsIds, int currentPage)
        {
            List<Transaction> TransactOfAllUserAccounts;

            if (currentPage == 1)
            {
                TransactOfAllUserAccounts = await this.context.Transactions
                                                             .Include(x => x.Type)
                                                             .Include(x => x.SenderAccount)
                                                             .ThenInclude(x => x.Client)
                                                             .Include(x => x.RecieverAccount)
                                                             .ThenInclude(x => x.Client)
                                                             .Where(t => accountsIds
                                                             .Contains(t.RecieverAccountId)
                                                             || accountsIds
                                                             .Contains(t.SenderAccountId))
                                                             .Where(p => p.Type.Name == "complete")
                                                             .OrderByDescending(t => t.Timestamp)
                                                             .Take(5)
                                                             .ToListAsync();

            }
            else
            {
                TransactOfAllUserAccounts = await this.context.Transactions
                                                             .Include(x => x.Type)
                                                             .Include(x => x.SenderAccount)
                                                             .ThenInclude(x => x.Client)
                                                             .Include(x => x.RecieverAccount)
                                                             .ThenInclude(x => x.Client)
                                                             .Where(t => accountsIds
                                                             .Contains(t.RecieverAccountId)
                                                             || accountsIds
                                                             .Contains(t.SenderAccountId))
                                                             .Where(p => p.Type.Name == "complete")
                                                             .OrderByDescending(t => t.Timestamp)
                                                             .Skip((currentPage - 1) * 5)
                                                             .Take(5)
                                                             .ToListAsync();
            }


            var transactionsDTO = this.allTransactionsDTOMapper.MapFrom(TransactOfAllUserAccounts);

            return transactionsDTO;
        }

        public async Task<int> GetPageCountForCompleteTransactions(List<Guid> accountsIds)
        {
            var count = await this.context.Transactions
                .Include(x => x.Type)
                .Include(x => x.SenderAccount)
                .ThenInclude(x => x.Client)
                .Include(x => x.RecieverAccount)
                .ThenInclude(x => x.Client)
                .Where(t => accountsIds
                .Contains(t.RecieverAccountId)
                 || accountsIds
                .Contains(t.SenderAccountId))
                .Where(p => p.Type.Name == "complete")
                .CountAsync();
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

        public ICollection<Account> GetAllAccounts(string searchItem)
        {
            return this.context.Accounts
               .Where(x => x.AccountNumber.Contains(searchItem))
               .ToList();
        }

        public ICollection<Account> GetAllAccountsForUser(string searchItem, string userId)
        {
            return this.context.UsersAccounts
                        .Where(x => x.UserId.ToString() == userId)
                        .Select(x => x.Account)
                        .Where(x => x.AccountNumber.Contains(searchItem))
                        .ToList();
        }

        public Account GetAccountById(Guid accountId)
        {
            return this.context.Accounts.Include(x => x.Client).FirstOrDefault(x => x.Id == accountId);
        }

        public Transaction GetTransactionById(Guid transactionId)
        {
            return this.context.Transactions
                .Include(p => p.SenderAccount)
                .ThenInclude(p => p.Client)
                .Include(p => p.RecieverAccount)
                .ThenInclude(p => p.Client)
                .FirstOrDefault(x => x.Id == transactionId);
        }

        public Transaction UpdateTransaction(Guid transactionId, Guid payerAccountId, Guid payeeAccountId, string description, decimal ammount)
        {
            var transaction = this.context.Transactions
                .Include(p => p.SenderAccount)
                .Include(p => p.RecieverAccount)
                .FirstOrDefault(x => x.Id == transactionId);

            transaction.SenderAccountId = payerAccountId;
            transaction.RecieverAccountId = payeeAccountId;
            transaction.Description = description;
            transaction.Amount = ammount;

            this.context.Transactions.Update(transaction);
            this.context.SaveChanges();

            return transaction;


        }

    }
}
