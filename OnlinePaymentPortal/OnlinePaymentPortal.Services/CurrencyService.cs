using Microsoft.EntityFrameworkCore;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services
{
    public class CurrencyService: ICurrencyService
    {
        private readonly ApplicationDbContext context;
        private readonly IDtoMapper<IReadOnlyCollection<Currency>, CollectionsDTO> allCurrenciesDTOMapper;

        public CurrencyService(ApplicationDbContext context,
            IDtoMapper<IReadOnlyCollection<Currency>, CollectionsDTO> allCurrenciesDTOMapper)
        {
            this.context = context;
            this.allCurrenciesDTOMapper = allCurrenciesDTOMapper;
        }

        public async Task<CollectionsDTO> GetAllCurrenciesAsync()
        {
            var currencies = await this.context.Currencies
               .ToListAsync();

            var currenciesDTO=this.allCurrenciesDTOMapper.MapFrom(currencies);
            return currenciesDTO;
        }

        //public async Task<Currency> FindCurrencyByNameAsync(string name)
        //{
        //    return await this.context.Currencies.FirstOrDefaultAsync(c => c.Name == name);
        //}
    }
}
