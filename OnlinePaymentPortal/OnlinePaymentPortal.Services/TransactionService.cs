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
    public class TransactionService: ITransactionService
    {
        private readonly ApplicationDbContext context;
        private readonly IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO> alltransactionsDTOMapper;

        public TransactionService(ApplicationDbContext context,
            IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO> alltransactionsDTOMapper)
        {
            this.context = context;
            this.alltransactionsDTOMapper = alltransactionsDTOMapper;
        }

        public async Task<CollectionsDTO> GetAllUserTransactionsAsync(List<Guid> accountsIds)
        {
            var TransactOfAllUserAccounts = await this.context.Transactions
                                                             .Include(x => x.Type)
                                                             .Include(x => x.SenderAccount)
                                                             //.ThenInclude(x => x.ClientsAccounts)
                                                             .ThenInclude(x => x.Client)
                                                             .Include(x => x.RecieverAccount)
                                                             //.ThenInclude(x => x.ClientsAccounts)
                                                             .ThenInclude(x => x.Client)
                                                             .Where(t => accountsIds
                                                             .Contains(t.RecieverAccountId)
                                                             || accountsIds
                                                             .Contains(t.SenderAccountId))
                                                             .ToListAsync();

            var accountsDTO = this.alltransactionsDTOMapper.MapFrom(TransactOfAllUserAccounts);

            return accountsDTO;
        }

    }
}
