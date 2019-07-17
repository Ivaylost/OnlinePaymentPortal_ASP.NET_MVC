using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class UsersAccounts
    {
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }

        public Account Account { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
