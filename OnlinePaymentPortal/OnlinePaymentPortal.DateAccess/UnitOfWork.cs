//using Microsoft.EntityFrameworkCore;
//using OnlinePaymentPortal.Data;
//using OnlinePaymentPortal.Data.Models;
//using OnlinePaymentPortal.DateAccess.Contracts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;


//namespace OnlinePaymentPortal.DateAccess
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        private readonly ApplicationDbContext context;
//        private readonly IGenericRepository<Account> accountRepository;
//        private readonly IGenericRepository<Balance> balanceRepository;
//        private readonly IGenericRepository<Banner> bannerRepository;
//        private readonly IGenericRepository<Client> clientRepository;
//        private readonly IGenericRepository<Currency> currencytRepository;
//        private readonly IGenericRepository<Rate> ratetRepository;
//        private readonly IGenericRepository<Role> roleRepository;
//        private readonly IGenericRepository<Transaction> transactionRepository;
//        private readonly IGenericRepository<User> userRepository;
//        private bool disposed = false;

//        public UnitOfWork(ApplicationDbContext context,
//            IGenericRepository<Account> accountRepository,
//                IGenericRepository<Balance> balanceRepository,
//                IGenericRepository<Banner> bannerRepository,
//                IGenericRepository<Client> clientRepository,
//                IGenericRepository<Currency> currencytRepository,
//                IGenericRepository<Rate> ratetRepository,
//                IGenericRepository<Role> roleRepository,
//                IGenericRepository<Transaction> transactionRepository,
//                IGenericRepository<User> userRepository)
//        {
//            this.context = context;
//            this.accountRepository = new GenericRepository<Account>(context);
//            this.balanceRepository = new GenericRepository<Balance>(context);
//            this.bannerRepository = new GenericRepository<Banner>(context);
//            this.clientRepository = new GenericRepository<Client>(context);
//            this.currencytRepository = new GenericRepository<Currency>(context);
//            this.ratetRepository = new GenericRepository<Rate>(context);
//            this.roleRepository = new GenericRepository<Role>(context);
//            this.transactionRepository = new GenericRepository<Transaction>(context);
//            this.userRepository = new GenericRepository<User>(context);
//        }



//        public IGenericRepository<Account> AccountRepository
//        {
//            get
//            {
//                return this.accountRepository;
//            }
//        }
//        public IGenericRepository<Balance> BalanceRepository
//        {
//            get
//            {
//                return this.balanceRepository;
//            }
//        }
//        public IGenericRepository<Banner> BannerRepository
//        {
//            get
//            {
//                return this.bannerRepository;
//            }
//        }
//        public IGenericRepository<Client> ClientRepository
//        {
//            get
//            {
//                return this.clientRepository;
//            }
//        }
//        public IGenericRepository<Currency> CurrencytRepository
//        {
//            get
//            {
//                return this.currencytRepository;
//            }
//        }
//        public IGenericRepository<Rate> RatetRepository
//        {
//            get
//            {
//                return this.ratetRepository;
//            }
//        }
//        public IGenericRepository<Role> RoleRepository
//        {
//            get
//            {
//                return this.roleRepository;
//            }
//        }
//        public IGenericRepository<Transaction> TransactionRepository
//        {
//            get
//            {
//                return this.transactionRepository;
//            }
//        }
//        public IGenericRepository<User> UserRepository
//        {
//            get
//            {
//                return this.userRepository;
//            }
//        }

//        public void Save()
//        {
//            context.SaveChanges();
//        }

//        public void Rollback()
//        {
//            {
//                var changedEntries = context.ChangeTracker.Entries()
//                    .Where(x => x.State != EntityState.Unchanged).ToList();

//                foreach (var entry in changedEntries)
//                {
//                    switch (entry.State)
//                    {
//                        case EntityState.Modified:
//                            entry.CurrentValues.SetValues(entry.OriginalValues);
//                            entry.State = EntityState.Unchanged;
//                            break;
//                        case EntityState.Added:
//                            entry.State = EntityState.Detached;
//                            break;
//                        case EntityState.Deleted:
//                            entry.State = EntityState.Unchanged;
//                            break;
//                    }
//                }
//            }
//        }

        

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    context.Dispose();
//                }
//            }
//            this.disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}
