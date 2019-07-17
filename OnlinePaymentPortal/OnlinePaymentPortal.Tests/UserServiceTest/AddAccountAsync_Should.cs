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
    public class AddAccountAsync_Should
    {
        [TestMethod]
        public async Task Add_Account_To_User()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Add_Account_To_User));
            //var userDTOMapperMock = new Mock<IDtoMapper<User, UserDTO>>();
            //var hashService = new Mock<IHashService>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Accounts.Add(TestUtils.account1);
                arrangeContext.Users.Add(TestUtils.user);

                var sut = new UserService(arrangeContext, null, null, null, null, null, null, null);
                var result = await sut.AddAccountAsync(TestUtils.user.Id, TestUtils.account1.Id);
                Assert.AreEqual(result.AccountId, TestUtils.account1.Id);
                Assert.AreEqual(result.UserId, TestUtils.user.Id);
            }
        }
    }
}
