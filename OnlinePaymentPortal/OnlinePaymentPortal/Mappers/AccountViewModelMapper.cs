using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Mappers
{
    public class AccountViewModelMapper : IViewModelMapper<AccountDTO, AccountViewModel>
    {
        public AccountViewModel MapFrom(AccountDTO entity)
        {
            return new AccountViewModel
            {
                Id = entity.Id,
                AccountNumber = entity.AccountNumber,
                ClientName = entity.ClientName,
                BalanceValue = entity.BalanceValue,
                CurrencyName = entity.CurrencyName,
                NickName=entity.NickName
            };
        }
    }
}
