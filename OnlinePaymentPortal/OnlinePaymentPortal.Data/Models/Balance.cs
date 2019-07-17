using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class Balance
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        [ForeignKey("Currency")]
        public Guid CurrencyId { get; set; }

        public Currency Currency { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        public Account Account { get; set; }
    }
}
