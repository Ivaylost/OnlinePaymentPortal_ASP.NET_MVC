using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class Account 
    {
        [Key]
        public Guid Id { get; set; }

        public string AccountNumber { get; set; }

        public string Nickname { get; set; }

        [ForeignKey("Client")]
        public Guid ClientId { get; set; }

        public Client Client { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<UsersAccounts> UsersAccounts { get; set; }

        public ICollection<Balance> Balances { get; set; }

        public ICollection<Transaction> SenderTransactions { get; set; }

        public ICollection<Transaction> RecieverTransaction { get; set; }
    }
}
