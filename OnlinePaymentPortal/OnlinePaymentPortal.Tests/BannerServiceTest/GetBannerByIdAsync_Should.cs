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
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.BannerServiceTest
{
    [TestClass]
    public class GetBannerByIdAsync_Should
    {
        [TestMethod]
        public async Task Return_Banner_By_Id()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Banner_By_Id));
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Banners.Add(TestUtils.banner1);
                arrangeContext.Banners.Add(TestUtils.banner2);
                await arrangeContext.SaveChangesAsync();
                var sut = new BannerService(arrangeContext, null, null);
                var banner = sut.GetBannerByIdAsync(TestUtils.banner1.Id);
                Assert.IsInstanceOfType(banner.Result, typeof(Banner));
                Assert.AreEqual(banner.Result.Id, TestUtils.banner1.Id);
            }
        }
    }
}
