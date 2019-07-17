using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class TransactionDTOMapper : IDtoMapper<Transaction, TransactionDTO>
    {
        public TransactionDTO MapFrom(Transaction entity)
        {
            return new TransactionDTO
            {
                Id = entity.Id,
                PayerAccount = entity.SenderAccount?.AccountNumber,
                PayerClientName = entity.SenderAccount?.Client.Name,
                PayeeAccount = entity.RecieverAccount?.AccountNumber,
                PayeeClientName = entity.RecieverAccount?.Client.Name,
                PaymentDescription = entity.Description,
                PaymentTimestamp = entity.Timestamp,
                PaymentStatus = entity.Type?.Name,
                Ammount = entity.Amount
            };
        }
    }
}
