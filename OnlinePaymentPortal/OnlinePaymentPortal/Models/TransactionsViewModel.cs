using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Models
{
    public class TransactionsViewModel
    {
        public Guid Id { get; set; }

        public string PayerAccount { get; set; }
        public Guid PayerAccountId { get; set; }

        public string PayerClientName { get; set; }

        public string PayeeAccount { get; set; }
        public Guid PayeeAccountId { get; set; }

        public string PayeeClientName { get; set; }

        public string PaymentDescription { get; set; }

        public decimal Ammount { get; set; }

        public DateTime PaymentTimestamp { get; set; }

        public string PaymentStatus { get; set; }
    }
}
