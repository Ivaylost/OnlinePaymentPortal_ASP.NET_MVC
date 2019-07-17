using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Account")]
        public Guid SenderAccountId { get; set; }

        public Account SenderAccount { get; set; }

        [ForeignKey("Account")]
        public Guid RecieverAccountId { get; set; }

        public Account RecieverAccount { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Timestamp { get; set; }

        [ForeignKey("Type")]
        public Guid TypeId { get; set; }

        public TransactionType Type { get; set; }

    }
}
