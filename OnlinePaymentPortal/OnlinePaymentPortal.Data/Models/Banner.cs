using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlinePaymentPortal.Data.Models
{
    public class Banner
    {
        [Key]
        public Guid Id { get; set; }

        public string ImagePath { get; set; }

        public string BannerLink { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
