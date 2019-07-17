using OnlinePaymentPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Models
{
    public class DetailsViewModels
    {
        public Guid Id { get; set; }

        public string ImagePath { get; set; }

        public string BannerLink { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
