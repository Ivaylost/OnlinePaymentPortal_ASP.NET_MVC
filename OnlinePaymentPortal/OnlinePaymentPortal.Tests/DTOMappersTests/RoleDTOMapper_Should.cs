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
    public class RoleDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_RoleDTO()
        {
            var role = TestUtils.roleFull;
            var result = new RoleDTOMapper().MapFrom(role);

            Assert.IsInstanceOfType(result, typeof(RoleDTO));
            Assert.AreEqual(result.Id, role.Id);
            Assert.AreEqual(result.Name, role.Name);
        }
    }
}
