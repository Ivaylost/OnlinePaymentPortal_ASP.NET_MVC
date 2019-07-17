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

namespace OnlinePaymentPortal.Tests.AdminServiceTest
{
    [TestClass]
    public class AdminService_Should
    {
        [TestMethod]
        public async Task Return_Null_If_Admin_Do_Not_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Null_If_Admin_Do_Not_Exist));
            var adminDTOMapperMock = new Mock<IDtoMapper<Admin, AdminDTO>>();
            var clientDto = new ClientDTO();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Admins.Add(TestUtils.adminFull);
                await arrangeContext.SaveChangesAsync();
                var sut = new AdminService(arrangeContext, adminDTOMapperMock.Object);
                var result = await sut.FindAdminAsync("ivan", "password");
                Assert.AreEqual(result,null);
            }
        }

        [TestMethod]
        public async Task Return_Admin_If_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Admin_If_Exist));
            var adminDTOMapperMock = new Mock<IDtoMapper<Admin, AdminDTO>>();
            var clientDto = new ClientDTO();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Admins.Add(TestUtils.adminFull);
                await arrangeContext.SaveChangesAsync();
                var sut = new AdminService(arrangeContext, adminDTOMapperMock.Object);
                var result = await sut.FindAdminAsync(TestUtils.adminFull.UserName, TestUtils.adminFull.PasswordHash);
                adminDTOMapperMock.Verify(x => x.MapFrom(It.Is<Admin>(xx => xx.UserName == TestUtils.adminFull.UserName)));
            }
        }
    }
}
