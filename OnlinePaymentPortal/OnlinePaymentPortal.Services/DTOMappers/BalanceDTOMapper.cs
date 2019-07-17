using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class BalanceDTOMapper: IDtoMapper<Balance, BalanceDTO>
    {
        public BalanceDTO MapFrom(Balance entity)
        {
            return new BalanceDTO
            {
                Id = entity.Id,
                Value = entity.Value,
                CurrencyId = entity.CurrencyId,
                AccountId = entity.AccountId
            };
        }
    }
}
