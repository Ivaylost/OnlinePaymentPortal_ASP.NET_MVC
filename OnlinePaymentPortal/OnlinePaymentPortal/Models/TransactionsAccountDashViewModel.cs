using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Models
{
    public class TransactionsAccountDashViewModel
    {
        public IReadOnlyList<TransactionsAccountViewModel> UserAccountsWithTransactions { get; set; }
    }
}
