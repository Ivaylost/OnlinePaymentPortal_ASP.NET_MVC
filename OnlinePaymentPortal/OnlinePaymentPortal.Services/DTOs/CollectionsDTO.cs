using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOs
{
    public class CollectionsDTO
    {
        public IReadOnlyList<ClientDTO> Clients { get; set; }
        public IReadOnlyList<BannerDTO> Banners { get; set; }
        public IReadOnlyList<AccountDTO> Accounts { get; set; }
        public IReadOnlyList<UserDTO> Users { get; set; }
        public IReadOnlyList<TransactionDTO> Transactions { get; set; }
        public IReadOnlyList<CurrencyDTO> Currencies { get; set; }

    }
}
