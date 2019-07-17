using Microsoft.Extensions.DependencyInjection;
using OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces;
using OnlinePaymentPortal.Areas.Administration.Models;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Mappers
{
    public static class MapperRegistration
    {
        public static IServiceCollection AddCustomMappers(this IServiceCollection services)
        {
            services.AddScoped<IViewModelMapper<IReadOnlyCollection<UserDTO>, AdminViewModel>, AdminViewModelMapper>();
            services.AddScoped<IViewModelMapper<UserDTO, UserViewModel>, UserViewModelMapper>();
            services.AddScoped<IViewModelMapper<IReadOnlyCollection<ClientDTO>, AdminViewModel>, AdminViewModelMapper>();
            services.AddScoped<IViewModelMapper<ClientDTO, ClientViewModel>, ClientViewModelMapper>();
            services.AddScoped<IViewModelMapper<IReadOnlyCollection<BannerDTO>, AdminViewModel>, AdminViewModelMapper>();
            services.AddScoped<IViewModelMapper<BannerDTO, BannerViewModel>, BannerViewModelMapper>();
            services.AddScoped<IViewModelMapper<IReadOnlyCollection<AccountDTO>, AdminViewModel>, AdminViewModelMapper>();
            services.AddScoped<IViewModelMapper<AccountDTO, AccountViewModel>, AccountViewModelMapper>();

            services.AddScoped<IViewModelMapper<Banner, DetailsViewModels>, DetailViewModelMapper>();

            return services;
        }
    }
}
