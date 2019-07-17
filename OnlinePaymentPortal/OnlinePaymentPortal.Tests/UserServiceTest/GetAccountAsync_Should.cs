using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;
using OnlinePaymentPortal.Tests.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.UserServiceTest
{
    [TestClass]
    public class GetAccountAsync_Should
    {
        [TestMethod]
        public async Task Get_User_Account()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Get_User_Account));
            var userDTOMapper = new Mock<IDtoMapper<User, UserDTO>>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Accounts.Add(TestUtils.receiverAccount);
                await arrangeContext.SaveChangesAsync();

                var sut = new UserService(arrangeContext, null, null, null, null, userDTOMapper.Object, null, null);
                var result = await sut.GetAccountAsync(TestUtils.receiverAccount.Id);
                Assert.IsInstanceOfType(result, typeof(Account));
                Assert.AreEqual(result.Id, TestUtils.receiverAccount.Id);
            }
        }
    }
}
