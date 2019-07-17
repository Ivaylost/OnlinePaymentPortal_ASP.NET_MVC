using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services.Interfaces
{
    public interface ICurrencyService
    {
        //TODO??
        //Task<Currency> FindCurrencyByNameAsync(string name);
        Task<CollectionsDTO> GetAllCurrenciesAsync();
    }
}
