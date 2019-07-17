using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Models
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Must be a maximum of 32 characters")]
        public string Name { get; set; }
    }
}
