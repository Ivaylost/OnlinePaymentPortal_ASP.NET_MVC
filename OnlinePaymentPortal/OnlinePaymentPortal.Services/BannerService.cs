using Microsoft.EntityFrameworkCore;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services
{
    public class BannerService : IBannerService
    {
        private readonly ApplicationDbContext context;
        private readonly IDtoMapper<Banner, BannerDTO> bannerDTOMapper;
        private readonly IDtoMapper<IReadOnlyCollection<Banner>, CollectionsDTO> allBannersDTOMapper;

        public BannerService(ApplicationDbContext context,
            IDtoMapper<Banner, BannerDTO> bannerDTOMapper,
            IDtoMapper<IReadOnlyCollection<Banner>, CollectionsDTO> allBannersDTOMapper)
        {
            this.context = context;
            this.bannerDTOMapper = bannerDTOMapper;
            this.allBannersDTOMapper = allBannersDTOMapper;
        }

        public async Task<Banner> GetBannerByIdAsync(Guid id)
        {
            return await this.context.Banners.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BannerDTO> AddBannerAsync(string imagePath, string startDate, string endDate,string bannerLink)
        {

            var banner = new Banner()
            {
                ImagePath = imagePath,
                StartDate = DateTime.Parse(startDate),
                EndDate = DateTime.Parse(endDate),
                BannerLink=bannerLink,
                CreatedOn = DateTime.Now
            };


            await this.context.Banners.AddAsync(banner);
            await this.context.SaveChangesAsync();
            var bannerDTO = this.bannerDTOMapper.MapFrom(banner);
            return bannerDTO;
        }

        //TODO:
        public async Task<Banner> UpdateBannerAsync(Guid Id,string imagePath, string BannerLink, DateTime StartDate, DateTime EndDate)
        {
            var banner =await this.context.Banners.FirstOrDefaultAsync(p => p.Id == Id);
            banner.ImagePath = imagePath;
            banner.BannerLink = BannerLink;
            banner.StartDate = StartDate;
            banner.EndDate = EndDate;

            this.context.Banners.Update(banner);
            await this.context.SaveChangesAsync();
            return banner;
        }

        public async Task<IReadOnlyCollection<Banner>> GetAllBannersAsync()
        {
            return await this.context.Banners
               .ToListAsync();
        }

        public async Task<CollectionsDTO> GetAllBannersAsync(int currentPage)
        {
            List<Banner> banners;

            if (currentPage == 1)
            {
                banners = await this.context
                    .Banners
                    .OrderByDescending(c => c.CreatedOn)
                    .Take(9)
                    .ToListAsync();

            }
            else
            {
                banners = await this.context
                    .Banners
                    .OrderByDescending(c => c.CreatedOn)
                    .Skip((currentPage - 1) * 9)
                    .Take(9)
                    .ToListAsync();
            }

            return this.allBannersDTOMapper.MapFrom(banners);
        }

        public async Task<int> GetPageCount()
        {
            var count = await this.context.Banners.CountAsync();
            int pages = 0;

            if (count % 9 == 0)
            {
                pages = (count / 9);
            }
            else
            {
                pages = (count / 9) + 1;
            }
            return pages;
        }

        public Banner GetBanner()
        {
            var date = DateTime.Now;
            var banners = this.context.Banners.Where(u => u.EndDate >= date).ToList();
            if (banners.Count() == 0)
            {
                return null;
            }
            Random random = new Random();
            int num = random.Next(0, banners.Count);
            return banners[num];
        }

        public async Task<bool> DeleteBannerAsync(Banner banner)
        {
            bool isTrue;
            this.context.Banners.Remove(banner);
            await this.context.SaveChangesAsync();

            isTrue = true;
            return isTrue;
        }
    }
}