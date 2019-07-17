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
    public class GetBanner_Should
    {
        [TestMethod]
        public void Return_Banners()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Banners));

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Banners.Add(TestUtils.banner1);
                arrangeContext.Banners.Add(TestUtils.banner2);
                arrangeContext.SaveChanges();
                var sut = new BannerService(arrangeContext, null, null);
                var result = sut.GetBanner();
                Assert.IsInstanceOfType(result, typeof(Banner));
                Assert.AreNotEqual(result, null);
            }
        }

        [TestMethod]
        public void Return_NULL_If_No_Bnaer()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_NULL_If_No_Bnaer));

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var sut = new BannerService(arrangeContext, null, null);
                var result = sut.GetBanner();
                Assert.AreEqual(result, null);
            }
        }
    }
}
