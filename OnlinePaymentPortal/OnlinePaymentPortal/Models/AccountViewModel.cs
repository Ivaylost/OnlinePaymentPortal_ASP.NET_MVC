using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Models
{
    public class AccountViewModel
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
