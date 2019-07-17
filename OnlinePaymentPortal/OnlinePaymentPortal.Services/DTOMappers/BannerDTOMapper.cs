using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class BannerDTOMapper : IDtoMapper<Banner, BannerDTO>
    {
        public BannerDTO MapFrom(Banner entity)
        {
            return new BannerDTO
            {
                Id = entity.Id,
                ImagePath = entity.ImagePath,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }
    }
}
