using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOs
{
    public class AccountDTO
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid CurrencyId { get; set; }

        public string AccountNumber { get; set; }

        public string ClientName { get; set; }

        public decimal BalanceValue { get; set; }

        public string CurrencyName { get; set; }

        public string NickName { get; set; }
    }
}
