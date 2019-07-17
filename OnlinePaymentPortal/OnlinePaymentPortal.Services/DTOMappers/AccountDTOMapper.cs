using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class AccountDTOMapper : IDtoMapper<Account, AccountDTO>
    {
        public AccountDTO MapFrom(Account entity)
        {
            return new AccountDTO
            {
                Id = entity.Id,
                AccountNumber = entity.AccountNumber,
                ClientId = entity.ClientId,
                ClientName=entity.Client.Name,
                BalanceValue = entity.Balances.FirstOrDefault(p => p.AccountId == entity.Id).Value,
                CurrencyName = entity.Balances?.FirstOrDefault(p => p.AccountId == entity.Id)?.Currency?.Name,
                NickName = entity.Nickname
            };
        }
    }
}
