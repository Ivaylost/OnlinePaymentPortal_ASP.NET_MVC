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
    public class UserDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_UserDTO()
        {
            var user = TestUtils.user;
            var result = new UserDTOMapper().MapFrom(user);

            Assert.IsInstanceOfType(result, typeof(UserDTO));
            Assert.AreEqual(result.Id, user.Id);
            Assert.AreEqual(result.Name, user.Name);
            Assert.AreEqual(result.UserName, user.UserName);
            Assert.AreEqual(result.Password, user.PasswordHash);
            Assert.AreEqual(result.RoleId, user.RoleId);
        }
    }
}
