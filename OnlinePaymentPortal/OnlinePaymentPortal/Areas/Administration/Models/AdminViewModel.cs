using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Models
{
    public class AdminViewModel
    {
        public IReadOnlyList<ClientViewModel> Clients { get; set; }
        public IReadOnlyList<UserViewModel> Users { get; set; }
        public IReadOnlyList<BannerViewModel> Banners { get; set; }
        public IReadOnlyList<AccountViewModel> Accounts { get; set; }

        public int? PreviousPage { get; set; }

        public int CurrentPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }
    }
}
