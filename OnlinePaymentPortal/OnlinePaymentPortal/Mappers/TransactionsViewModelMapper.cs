using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Models;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Mappers
{
    public class TransactionsViewModelMapper : IViewModelMapper<TransactionDTO, TransactionsViewModel>
    {
        public TransactionsViewModel MapFrom(TransactionDTO entity)
        {
            return new TransactionsViewModel
            {
                Id = entity.Id,
                PayerAccount = entity.PayerAccount,
                PayerClientName = entity.PayerClientName,
                PayeeAccount = entity.PayeeAccount,
                PayeeClientName = entity.PayeeClientName,
                PaymentDescription = entity.PaymentDescription,
                PaymentTimestamp = entity.PaymentTimestamp,
                PaymentStatus = entity.PaymentStatus,
                Ammount = entity.Ammount
            };
        }
    }
}
