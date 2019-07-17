using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<CollectionsDTO> GetAllUserTransactionsAsync(List<Guid> accountsIds);
    }
}
