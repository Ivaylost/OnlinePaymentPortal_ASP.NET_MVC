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
    public class AdminDTOMapper_Schould
    {
        [TestMethod]
        public void Map_Correct_AdminDTO()
        {
            var admin = TestUtils.adminFull;
            var result = new AdminDTOMapper().MapFrom(admin);

            Assert.IsInstanceOfType(result, typeof(AdminDTO));
            Assert.AreEqual(result.Id, admin.Id);
            Assert.AreEqual(result.Name,admin.Name);
            Assert.AreEqual(result.UserName, admin.UserName);
            Assert.AreEqual(result.Password, admin.PasswordHash);
            Assert.AreEqual(result.RoleId, admin.RoleId);
        }
    }
}
