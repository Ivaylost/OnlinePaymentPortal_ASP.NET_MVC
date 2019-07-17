using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlinePaymentPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Data.ModelConfigurations
{
    public class BalanceConfiguration: IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder
                .HasOne(m => m.Currency)
                .WithMany(t => t.Balance)
                .HasForeignKey(m => m.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
               .HasOne(m => m.Account)
               .WithMany(t => t.Balances)
               .HasForeignKey(m => m.AccountId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
        }
    }
}
