using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
