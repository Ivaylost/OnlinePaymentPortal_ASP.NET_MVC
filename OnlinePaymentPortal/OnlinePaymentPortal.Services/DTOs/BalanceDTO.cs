using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOs
{
    public class BalanceDTO
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public Guid CurrencyId { get; set; }

        public Guid AccountId { get; set; }

    }
}
