using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOs
{
    public class BannerDTO
    {
        public Guid Id { get; set; }

        public string ImagePath { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
