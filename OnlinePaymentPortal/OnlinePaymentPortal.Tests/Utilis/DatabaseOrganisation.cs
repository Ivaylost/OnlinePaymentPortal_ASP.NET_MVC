using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlinePaymentPortal.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Tests.Utilis
{
    public static class DatabaseOrganisation
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions(string databaseName)
        {
            var provider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(provider)
                .Options;
        }
    }
}
