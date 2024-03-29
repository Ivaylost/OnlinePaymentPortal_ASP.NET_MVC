﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Models
{
    public class TransactionsDashViewModel
    {
        public IReadOnlyList<TransactionsViewModel> AllAccountTransactions { get; set; }

        public int? PreviousPage { get; set; }

        public int CurrentPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }

    }
}
