using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Mappers
{
    public class AccountDashViewModelMapper : IViewModelMapper<IReadOnlyCollection<AccountDTO>, AccountDashViewModel>
    {
        private readonly IViewModelMapper<AccountDTO, AccountViewModel> accountMapper;

        public AccountDashViewModelMapper(
            IViewModelMapper<AccountDTO, AccountViewModel> accountMapper)
        {
            this.accountMapper = accountMapper ?? throw new ArgumentNullException(nameof(accountMapper));
        }


        public AccountDashViewModel MapFrom(IReadOnlyCollection<AccountDTO> entity)
        {
            return new AccountDashViewModel
            {
                AllUserAccounts = entity.Select(this.accountMapper.MapFrom).ToList()
            };
        }
    }
}
