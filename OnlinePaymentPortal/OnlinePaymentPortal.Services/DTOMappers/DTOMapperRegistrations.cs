using Microsoft.Extensions.DependencyInjection;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public static class DTOMapperRegistrations
    {
        public static IServiceCollection AddCustomDTOMappers(this IServiceCollection services)
        {
            services.AddScoped<IDtoMapper<Client, ClientDTO>, ClientDTOMapper>();
            services.AddScoped<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>, CollectionsDTOMapper>();
            services.AddScoped<IDtoMapper<Banner, BannerDTO>, BannerDTOMapper>();
            services.AddScoped<IDtoMapper<IReadOnlyCollection<Banner>, CollectionsDTO>, CollectionsDTOMapper>();
            services.AddScoped<IDtoMapper<Account, AccountDTO>, AccountDTOMapper>();
            services.AddScoped<IDtoMapper<IReadOnlyCollection<Account>, CollectionsDTO>, CollectionsDTOMapper>();
            services.AddScoped<IDtoMapper<User, UserDTO>, UserDTOMapper>();
            services.AddScoped<IDtoMapper<IReadOnlyCollection<User>, CollectionsDTO>, CollectionsDTOMapper>();
            services.AddScoped<IDtoMapper<Transaction, TransactionDTO>, TransactionDTOMapper>();
            services.AddScoped<IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO>, CollectionsDTOMapper>();
            services.AddScoped<IDtoMapper<Balance, BalanceDTO>, BalanceDTOMapper>();
            services.AddScoped<IDtoMapper<Role, RoleDTO>, RoleDTOMapper>();
            services.AddScoped<IDtoMapper<Admin, AdminDTO>, AdminDTOMapper>();
            services.AddScoped<IDtoMapper<Currency, CurrencyDTO>, CurrencyDTOMapper>();
            services.AddScoped<IDtoMapper<IReadOnlyCollection<Currency>, CollectionsDTO>, CollectionsDTOMapper>();



            return services;
        }
    }
}
