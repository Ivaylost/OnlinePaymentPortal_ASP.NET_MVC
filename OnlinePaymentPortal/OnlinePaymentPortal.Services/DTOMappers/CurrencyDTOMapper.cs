using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class CurrencyDTOMapper : IDtoMapper<Currency, CurrencyDTO>
    {
        public CurrencyDTO MapFrom(Currency entity)
        {
            return new CurrencyDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Sign=entity.Sign,
                Abbr=entity.Abbr,
                IsPrefixed=entity.IsPrefixed
            };
        }
    }
}
