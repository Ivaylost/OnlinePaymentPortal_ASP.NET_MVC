using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }

        public string PayerAccount { get; set; }

        public string PayerClientName { get; set; }

        public string PayeeAccount { get; set; }

        public string PayeeClientName { get; set; }

        public string PaymentDescription { get; set; }

        public decimal Ammount { get; set; }

        public DateTime PaymentTimestamp { get; set; }

        public string PaymentStatus { get; set; }
    }
}
