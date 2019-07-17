using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using OnlinePaymentPortal.Data.Models;


namespace OnlinePaymentPortal.Data.ModelConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Models.Transaction>
    {
        public void Configure(EntityTypeBuilder<Models.Transaction> builder)
        {
            builder
                 .HasOne(m => m.SenderAccount)
                 .WithMany(t => t.SenderTransactions)
                 .HasForeignKey(m => m.SenderAccountId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

            builder
                .HasOne(m => m.RecieverAccount)
                .WithMany(t => t.RecieverTransaction)
                .HasForeignKey(m => m.RecieverAccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
