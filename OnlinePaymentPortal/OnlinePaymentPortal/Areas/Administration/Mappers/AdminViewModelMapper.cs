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
    public class AdminViewModelMapper:
    IViewModelMapper<IReadOnlyCollection<ClientDTO>, AdminViewModel>,
    IViewModelMapper<IReadOnlyCollection<UserDTO>, AdminViewModel>,
    IViewModelMapper<IReadOnlyCollection<BannerDTO>, AdminViewModel>,
    IViewModelMapper<IReadOnlyCollection<AccountDTO>, AdminViewModel>
    {
        private readonly IViewModelMapper<ClientDTO, ClientViewModel> clientMapper;
        private readonly IViewModelMapper<UserDTO, UserViewModel> userMapper;
        private readonly IViewModelMapper<BannerDTO, BannerViewModel> bannerMapper;
        private readonly IViewModelMapper<AccountDTO, AccountViewModel> accountMapper;

        public AdminViewModelMapper(IViewModelMapper<ClientDTO, ClientViewModel> clientMapper,
            IViewModelMapper<UserDTO, UserViewModel> userMapper,
            IViewModelMapper<BannerDTO, BannerViewModel> bannerMapper,
            IViewModelMapper<AccountDTO, AccountViewModel> accountMapper
            )
        {
            this.clientMapper = clientMapper ?? throw new ArgumentNullException(nameof(clientMapper));
            this.userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
            this.bannerMapper = bannerMapper ?? throw new ArgumentNullException(nameof(bannerMapper));
            this.accountMapper = accountMapper ?? throw new ArgumentNullException(nameof(accountMapper));
        }

        public AdminViewModel MapFrom(IReadOnlyCollection<ClientDTO> entity)
        {
            return new AdminViewModel
            {
                Clients = entity.Select(this.clientMapper.MapFrom).ToList(),
            };
        }

        public AdminViewModel MapFrom(IReadOnlyCollection<UserDTO> entity)
        {
            return new AdminViewModel
            {
                Users = entity.Select(this.userMapper.MapFrom).ToList(),
            };
        }

        public AdminViewModel MapFrom(IReadOnlyCollection<BannerDTO> entity)
        {
            return new AdminViewModel
            {
                Banners = entity.Select(this.bannerMapper.MapFrom).ToList(),
            };
        }

        public AdminViewModel MapFrom(IReadOnlyCollection<AccountDTO> entity)
        {
            return new AdminViewModel
            {
                Accounts = entity.Select(this.accountMapper.MapFrom).ToList(),
            };
        }
    }
}
