using OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces;
using OnlinePaymentPortal.Areas.Administration.Models;
using OnlinePaymentPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Mappers
{
    public class DetailViewModelMapper : IViewModelMapper<Banner, DetailsViewModels>
    {
        public DetailsViewModels MapFrom(Banner entity)
        {
            return new DetailsViewModels
            {
                Id = entity.Id,
                ImagePath=entity.ImagePath,
                StartDate = entity.StartDate,
                EndDate=entity.EndDate,
                BannerLink=entity.BannerLink
            };
        }

    }
}
