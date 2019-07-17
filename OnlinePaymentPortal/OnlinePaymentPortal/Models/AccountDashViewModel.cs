using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Models
{
    public class AccountDashViewModel
    {
        public IReadOnlyList<AccountViewModel> AllUserAccounts { get; set; }

        //public IReadOnlyList<TransactionsViewModel> AllClientTransactionSend { get; set; }

        //public IReadOnlyList<TransactionsViewModel> AllClientTransactionReceive { get; set; }

        //public IReadOnlyList<TransactionsViewModel> AllClientTransaction { get; set; }
    }
}
