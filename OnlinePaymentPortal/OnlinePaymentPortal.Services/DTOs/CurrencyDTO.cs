using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOs
{
    public class CurrencyDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Sign { get; set; }

        public string Abbr { get; set; }

        public bool IsPrefixed { get; set; }
    }
}
