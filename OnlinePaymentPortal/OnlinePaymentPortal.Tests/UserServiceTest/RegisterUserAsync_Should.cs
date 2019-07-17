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
    public class RegisterUserAsync_Should
    {
        [TestMethod]
        public async Task Throw_Exeption_If_User_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Throw_Exeption_If_User_Exist));
            var userDTOMapperMock = new Mock<IDtoMapper<User, UserDTO>>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "ivan",
                    UserName = "ivan",
                    PasswordHash = "password",
                };
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
                var sut = new UserService(arrangeContext, null, null, null, null, userDTOMapperMock.Object, null, null);

                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await sut.RegisterUserAsync("ivan", "ivan", "password"));
                Assert.AreEqual("User ivan already exists", ex.Message);
            }
        }

        [TestMethod]
        public async Task Register_New_User()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Register_New_User));
            var userDTOMapperMock = new Mock<IDtoMapper<User, UserDTO>>();
            var hashService = new Mock<IHashService>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var roleId = Guid.NewGuid();

                var role = new Role()
                {
                    Id = roleId,
                    Name = "user",
                };
                arrangeContext.Roles.Add(role);
                await arrangeContext.SaveChangesAsync();
                var sut = new UserService(arrangeContext, hashService.Object, null, null, null, userDTOMapperMock.Object, null, null);

                var result = await sut.RegisterUserAsync("ivan", "ivan", "password");
                userDTOMapperMock.Verify(x => x.MapFrom(It.Is<User>(xx => xx.UserName == "ivan")));
            }
        }
    }
}
