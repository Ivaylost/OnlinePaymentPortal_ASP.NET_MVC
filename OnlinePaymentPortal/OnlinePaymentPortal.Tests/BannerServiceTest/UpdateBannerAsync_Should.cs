using Microsoft.EntityFrameworkCore;
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
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.BannerServiceTest
{
    [TestClass]
    public class UpdateBannerAsync_Should
    {
        [TestMethod]
        public async Task Update_Banner_Correct()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Update_Banner_Correct));
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Banners.Add(TestUtils.banner1);
                await arrangeContext.SaveChangesAsync();
            }

            //using (var assertContext = new ApplicationDbContext(options))
            //{
            //    var newBanner = new Banner {
            //        Id = Guid.Parse("3baff448-7c4f-4c86-b829-83c6e42ebd84"),
            //        ImagePath = "path",
            //        StartDate = DateTime.Parse("01.06.2018"),
            //        EndDate = DateTime.Parse("06.06.2019"),
            //    };
            //    var sut = new BannerService(assertContext, null, null);
            //    var updatedBanner = await sut.UpdateBannerAsync(TestUtils.banner1, newBanner);
            //    Assert.IsInstanceOfType(updatedBanner, typeof(Banner));
            //    Assert.AreEqual(assertContext.Banners.First().EndDate, DateTime.Parse("06/06/2019"));
            //}
        }
    }
}
