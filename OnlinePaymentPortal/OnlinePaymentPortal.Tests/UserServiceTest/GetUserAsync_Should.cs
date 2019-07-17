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
    public class GetUserAsync_Should
    {
        [TestMethod]
        public async Task Get_User()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Get_User));
            var userDTOMapper = new Mock<IDtoMapper<User, UserDTO>>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Users.Add(TestUtils.user);
                await arrangeContext.SaveChangesAsync();

                var sut = new UserService(arrangeContext, null, null, null, null, userDTOMapper.Object, null, null);
                var result = await sut.GetUserAsync(TestUtils.user.Id.ToString());
                userDTOMapper.Verify(x => x.MapFrom(It.Is<User>(xx => xx.Id == TestUtils.user.Id))); 
            }
        }
    }
}
