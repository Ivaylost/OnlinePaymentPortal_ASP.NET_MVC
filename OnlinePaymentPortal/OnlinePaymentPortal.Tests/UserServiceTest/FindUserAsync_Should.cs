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
    public class FindUserAsync_Should
    {
        [TestMethod]
        public async Task Return_Null_If_User_Do_Noy_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Null_If_User_Do_Noy_Exist));
            var userDTOMapperMock = new Mock<IDtoMapper<User, UserDTO>>();
            var hashService = new Mock<IHashService>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                
                var sut = new UserService(arrangeContext, hashService.Object, null, null, null, userDTOMapperMock.Object, null, null);
                var result = await sut.FindUserAsync("ivan", "password");
                Assert.AreEqual(result, null);
            }
        }
        [TestMethod]
        public async Task Find_User_If_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Find_User_If_Exist));
            var userDTOMapperMock = new Mock<IDtoMapper<User, UserDTO>>();
            var hashService = new Mock<IHashService>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var userId = Guid.NewGuid();

                var user = new User()
                {
                    Id = userId,
                    UserName = "ivan",
                    PasswordHash = "aZKX26tFSB2qQLQoYsvQyA5ycir34TXBF8zKSwT2NtVWv5+a",
                };
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
                hashService.Setup(x => x.IsPasswordValid("Admin123@", "aZKX26tFSB2qQLQoYsvQyA5ycir34TXBF8zKSwT2NtVWv5+a")).Returns(true);
                var sut = new UserService(arrangeContext, hashService.Object, null, null, null, userDTOMapperMock.Object, null, null);

                var result = await sut.FindUserAsync("ivan", "Admin123@");
                userDTOMapperMock.Verify(x => x.MapFrom(It.Is<User>(xx => xx.UserName == "ivan")));
            }
        }

        [TestMethod]
        public async Task Return_Null_If_Wrong_Password()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Null_If_Wrong_Password));
            var userDTOMapperMock = new Mock<IDtoMapper<User, UserDTO>>();
            var hashService = new Mock<IHashService>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var userId = Guid.NewGuid();

                var user = new User()
                {
                    Id = userId,
                    UserName = "ivan",
                    PasswordHash = "somePassword",
                };
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
                var sut = new UserService(arrangeContext, hashService.Object, null, null, null, userDTOMapperMock.Object, null, null);

                var result = await sut.FindUserAsync("ivan", "Admin123@");
                Assert.AreEqual(result, null);
            }
        }
    }
}
