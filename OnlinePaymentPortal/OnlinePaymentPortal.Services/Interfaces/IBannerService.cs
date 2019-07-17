using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services.Interfaces
{
    public interface IBannerService
    {
        Task<IReadOnlyCollection<Banner>> GetAllBannersAsync();
        Task<BannerDTO> AddBannerAsync(string imagePath, string startDate, string endDate,string bannerLink);
        Task<Banner> GetBannerByIdAsync(Guid id);
        Banner GetBanner();
        Task<bool> DeleteBannerAsync(Banner banner);
        Task<CollectionsDTO> GetAllBannersAsync(int currentPage);
        Task<int> GetPageCount();
        Task<Banner> UpdateBannerAsync(Guid Id, string imagePath, string BannerLink, DateTime StartDate, DateTime EndDate);
    }
}
