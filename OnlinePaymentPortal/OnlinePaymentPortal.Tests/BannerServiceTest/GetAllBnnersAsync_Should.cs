using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Tests.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.BannerServiceTest
{
    [TestClass]
    public class GetAllBnnersAsync_Should
    {
        [TestMethod]
        public async Task Return_All_Banners()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_All_Banners));
            var accountDTOMapper = new Mock<IDtoMapper<Account, AccountDTO>>();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Banners.Add(TestUtils.banner1);
                arrangeContext.Banners.Add(TestUtils.banner2);
                await arrangeContext.SaveChangesAsync();

                var allBannersDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Banner>, CollectionsDTO>>();
                var sut = new BannerService(arrangeContext, null, allBannersDTOMapperMock.Object);
                var result = await sut.GetAllBannersAsync(1);
                allBannersDTOMapperMock.Verify(x => x.MapFrom(It.Is<List<Banner>>(xx => xx.Count() == 2)));
            }
        }

        [TestMethod]
        public async Task Return_All_Banners_Paging()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_All_Banners_Paging));
            var accountDTOMapper = new Mock<IDtoMapper<Account, AccountDTO>>();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 15; i++)
                {
                    var name = new Banner()
                    {
                        Id = Guid.NewGuid(),
                    };
                    arrangeContext.Banners.Add(name);
                }
                await arrangeContext.SaveChangesAsync();

                var allBannersDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Banner>, CollectionsDTO>>();
                var sut = new BannerService(arrangeContext, null, allBannersDTOMapperMock.Object);
                var result = await sut.GetAllBannersAsync(2);
                allBannersDTOMapperMock.Verify(x => x.MapFrom(It.Is<List<Banner>>(xx => xx.Count() == 6)));
            }

        }
    }
}
