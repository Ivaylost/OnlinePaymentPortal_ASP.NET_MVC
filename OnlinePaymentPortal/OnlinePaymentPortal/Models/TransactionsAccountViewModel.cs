using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlinePaymentPortal.Models
{
    public class TransactionsAccountViewModel
    {
        public string AccountForTransaction { get; set; }

        public IReadOnlyCollection<Transaction> AccountTransaction { get; set; }
    }
}
