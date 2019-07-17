using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlinePaymentPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Data.ModelConfigurations
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder
               .HasOne(m => m.BaseCurrency)
               .WithMany(t => t.BaseRates)
               .HasForeignKey(m => m.BaseCurrencyId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder
                .HasOne(m => m.ToCurrency)
                .WithMany(t => t.ToRates)
                .HasForeignKey(m => m.ToCurrencyId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
