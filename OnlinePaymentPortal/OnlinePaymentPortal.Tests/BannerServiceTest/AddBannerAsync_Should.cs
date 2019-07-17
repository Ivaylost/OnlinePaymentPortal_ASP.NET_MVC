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
    public class AddBannerAsync_Should
    {
        [TestMethod]
        public async Task Add_Banner()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Add_Banner));
            var bannerDTOMapper = new Mock<IDtoMapper<Banner, BannerDTO>>();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var bannerDTOMapperMock = new Mock<IDtoMapper<Banner, BannerDTO>> ();
                bannerDTOMapperMock.Setup(x => x.MapFrom(It.IsAny<Banner>())).Returns(TestUtils.bannerDTO1);
                var sut = new BannerService(arrangeContext, bannerDTOMapperMock.Object, null);
                var banner = await sut.AddBannerAsync("path", "01.01.2018" , "01.01.2019", "bmw.bg");
                Assert.IsInstanceOfType(banner, typeof(BannerDTO));
                Assert.AreEqual(arrangeContext.Banners.First().ImagePath, "path");
                Assert.AreEqual(arrangeContext.Banners.First().StartDate, DateTime.Parse("01.01.2018"));
                Assert.AreEqual(arrangeContext.Banners.First().EndDate, DateTime.Parse("01.01.2019"));
            }
        }
    }
}
