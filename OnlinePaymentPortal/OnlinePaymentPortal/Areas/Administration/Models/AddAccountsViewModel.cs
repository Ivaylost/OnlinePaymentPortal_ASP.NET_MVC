using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Models
{
    public class AddAccountsViewModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public IEnumerable<SelectListItem> Accounts { get; set; }
    }
}
