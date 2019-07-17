using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Tests.Utilis;

namespace OnlinePaymentPortal.Tests.DTOMappersTests
{
    [TestClass]
    public class BannerDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_BannerDTO()
        {
            var banner = TestUtils.banner1;
            var result = new BannerDTOMapper().MapFrom(banner);

            Assert.IsInstanceOfType(result, typeof(BannerDTO));
            Assert.AreEqual(result.Id, banner.Id);
            Assert.AreEqual(result.ImagePath, banner.ImagePath);
            Assert.AreEqual(result.StartDate, banner.StartDate);
            Assert.AreEqual(result.EndDate, banner.EndDate);
        }
    }
}
