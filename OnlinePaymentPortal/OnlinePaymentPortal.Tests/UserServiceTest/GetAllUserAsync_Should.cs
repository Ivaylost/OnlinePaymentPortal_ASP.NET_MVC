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

namespace OnlinePaymentPortal.Tests.UserServiceTest
{
    [TestClass]
    public class GetAllUserAsync_Should
    {
        [TestMethod]
        public async Task Return_All_User_Page_One()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_All_User_Page_One));


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 11; i++)
                {
                    var guid = Guid.NewGuid();
                    var userNew = new User()
                    {
                        Id = guid,
                        Name = guid.ToString(),
                        CreatedOn = DateTime.Now,
                    };
                    arrangeContext.Users.Add(userNew);
                }

                await arrangeContext.SaveChangesAsync();
                var allUsersDTOMapper = new Mock<IDtoMapper<IReadOnlyCollection<User>, CollectionsDTO>>();
                var clientDto = new ClientDTO();
                var sut = new UserService(arrangeContext, null, null, allUsersDTOMapper.Object, null, null, null, null);
                var users = await sut.GetAllUsersAsync(1);
                allUsersDTOMapper.Verify(x => x.MapFrom(It.Is<List<User>>(xx => xx.Count() == 5)));
            }
        }

        [TestMethod]
        public async Task Return_All_User_Page_Two()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_All_User_Page_Two));


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 11; i++)
                {
                    var guid = Guid.NewGuid();
                    var userNew = new User()
                    {
                        Id = guid,
                        Name = guid.ToString(),
                        CreatedOn = DateTime.Now,
                    };
                    arrangeContext.Users.Add(userNew);
                }

                await arrangeContext.SaveChangesAsync();
                var allUsersDTOMapper = new Mock<IDtoMapper<IReadOnlyCollection<User>, CollectionsDTO>>();
                var clientDto = new ClientDTO();
                var sut = new UserService(arrangeContext, null, null, allUsersDTOMapper.Object, null, null, null, null);
                var users = await sut.GetAllUsersAsync(2);
                allUsersDTOMapper.Verify(x => x.MapFrom(It.Is<List<User>>(xx => xx.Count() == 5)));
            }
        }
    }
}
