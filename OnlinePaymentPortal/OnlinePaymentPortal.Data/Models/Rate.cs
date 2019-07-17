using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class Rate
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Currency")]
        public Guid BaseCurrencyId { get; set; }

        public Currency BaseCurrency { get; set; }

        [ForeignKey("Currency")]
        public Guid ToCurrencyId { get; set; }

        public Currency ToCurrency { get; set; }

        public decimal ExchangeRate { get; set; }
    }
}
