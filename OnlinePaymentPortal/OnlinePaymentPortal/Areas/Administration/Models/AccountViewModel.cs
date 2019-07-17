using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Models
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid CurrencyId { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public decimal BalanceValue { get; set; }

        [Required]
        public string CurrencyName { get; set; }

        public IEnumerable<SelectListItem> Clients { get; set; }

        public IEnumerable<SelectListItem> Currencies { get; set; }

    }
}
