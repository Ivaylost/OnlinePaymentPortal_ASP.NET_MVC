using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Mappers
{
    public class TransactionsDashViewModelMapper :
        IViewModelMapper<IReadOnlyCollection<TransactionDTO>, TransactionsDashViewModel>
    {
        private readonly IViewModelMapper<TransactionDTO, TransactionsViewModel> transactionMapper;

        public TransactionsDashViewModelMapper(
            IViewModelMapper<TransactionDTO, TransactionsViewModel> transactionMapper)
        {
            this.transactionMapper = transactionMapper ?? throw new ArgumentNullException(nameof(transactionMapper));
        }


        public TransactionsDashViewModel MapFrom(IReadOnlyCollection<TransactionDTO> entity)
        {
            return new TransactionsDashViewModel
            {
                AllAccountTransactions = entity.Select(this.transactionMapper.MapFrom).ToList()
            };
        }
    }
}
