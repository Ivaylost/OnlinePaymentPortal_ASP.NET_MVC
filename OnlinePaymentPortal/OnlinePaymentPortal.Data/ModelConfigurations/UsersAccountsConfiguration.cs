using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlinePaymentPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Data.ModelConfigurations
{
    public class UsersAccountsConfiguration: IEntityTypeConfiguration<UsersAccounts>
    {
        public void Configure(EntityTypeBuilder<UsersAccounts> builder)
        {
            builder
                .HasKey(o => new { o.UserId, o.AccountId });
        }

    }
}
