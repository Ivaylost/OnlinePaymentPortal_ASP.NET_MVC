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
    public class AccountViewModelMapper : IViewModelMapper<AccountDTO, AccountViewModel>
    {
        public AccountViewModel MapFrom(AccountDTO entity)
        => new AccountViewModel
        {
            Id=entity.Id,
            AccountNumber=entity.AccountNumber,
            ClientName=entity.ClientName,
            BalanceValue = entity.BalanceValue,
            CurrencyName = entity.CurrencyName
        };
    }
}
