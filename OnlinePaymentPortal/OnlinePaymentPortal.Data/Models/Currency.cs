using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class Currency
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Sign { get; set; }
            
        public string Abbr { get; set; }

        public bool IsPrefixed { get; set; }

        public ICollection<Balance> Balance { get; set; }

        public ICollection<Rate> BaseRates { get; set; }

        public ICollection<Rate> ToRates { get; set; }
    }
}
