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
    public class GetPageCount_Should
    {
        [TestMethod]
        public async Task Return_Number_Of_Pages_Odd()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Number_Of_Pages_Odd));

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 22; i++)
                {
                    var name = new Banner()
                    {
                        Id = Guid.NewGuid(),
                    };
                    arrangeContext.Banners.Add(name);
                }
                await arrangeContext.SaveChangesAsync();

                var sut = new BannerService(arrangeContext, null, null);
                var result = await sut.GetPageCount();
                Assert.AreEqual(result, 3);
            }
        }

        [TestMethod]
        public async Task Return_Number_Of_Pages_Even()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Number_Of_Pages_Even));

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 18; i++)
                {
                    var name = new Banner()
                    {
                        Id = Guid.NewGuid(),
                    };
                    arrangeContext.Banners.Add(name);
                }
                await arrangeContext.SaveChangesAsync();

                var sut = new BannerService(arrangeContext, null, null);
                var result = await sut.GetPageCount();
                Assert.AreEqual(result, 2);
            }
        }
    }
}
