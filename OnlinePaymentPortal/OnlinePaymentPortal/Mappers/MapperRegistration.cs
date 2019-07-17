using Microsoft.Extensions.DependencyInjection;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Mappers
{
    public static class MapperRegistration
    {
        public static IServiceCollection AddUserCustomMapper(this IServiceCollection services)
        {
            services.AddScoped<IViewModelMapper<AccountDTO, AccountViewModel>, AccountViewModelMapper>();
            services.AddScoped<IViewModelMapper<IReadOnlyCollection<AccountDTO>, AccountDashViewModel>, AccountDashViewModelMapper>();
            services.AddScoped<IViewModelMapper<IReadOnlyCollection<TransactionDTO>, TransactionsDashViewModel>, TransactionsDashViewModelMapper>();
            services.AddScoped<IViewModelMapper<TransactionDTO, TransactionsViewModel>, TransactionsViewModelMapper>();

            return services;
        }
    }
}
