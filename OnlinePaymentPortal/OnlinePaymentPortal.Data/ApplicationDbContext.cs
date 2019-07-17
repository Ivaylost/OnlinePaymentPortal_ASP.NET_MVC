using Microsoft.EntityFrameworkCore;
using OnlinePaymentPortal.Data.ModelConfigurations;
using OnlinePaymentPortal.Data.Models;

namespace OnlinePaymentPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Balance> Balances { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<UsersAccounts> UsersAccounts { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsersAccountsConfiguration());

            builder.ApplyConfiguration(new BalanceConfiguration());

            builder.ApplyConfiguration(new RateConfiguration());

            builder.ApplyConfiguration(new TransactionConfiguration());

            builder.Entity<Client>()
                .Property(b => b.Name)
                .IsRequired();

            builder.Entity<Client>()
            .HasIndex(u => u.Name)
            .IsUnique();
        }
    }
}
