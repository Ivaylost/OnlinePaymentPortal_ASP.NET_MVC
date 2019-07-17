using OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces;
using OnlinePaymentPortal.Areas.Administration.Models;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Mappers
{
    public class BannerViewModelMapper : IViewModelMapper<BannerDTO, BannerViewModel>
    {
        public BannerViewModel MapFrom(BannerDTO entity)
     => new BannerViewModel
     {
         Id = entity.Id,
         ImagePath = entity.ImagePath,
         StartDate=entity.StartDate,
         EndDate=entity.EndDate
     };
    }
}
