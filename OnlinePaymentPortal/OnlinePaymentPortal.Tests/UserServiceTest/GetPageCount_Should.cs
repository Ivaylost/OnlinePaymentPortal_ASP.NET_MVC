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
    public class GetPageCount_Should
    {
        [TestMethod]
        public async Task Return_Number_Of_Pages_Odd()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Number_Of_Pages_Odd));

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 10; i++)
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

                //var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
                //var allClientDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>>();
                //var clientDto = new ClientDTO();
                var sut = new UserService(arrangeContext, null, null, null, null, null, null, null);
                var users = await sut.GetPageCount();
                Assert.AreEqual(users, 2);
            }
        }

        [TestMethod]
        public async Task Return_Number_Of_Pages_Even()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Number_Of_Pages_Even));

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 3; i++)
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

                var sut = new UserService(arrangeContext, null, null, null, null, null, null, null);
                var users = await sut.GetPageCount();
                Assert.AreEqual(users, 1);
            }
        }
    }
}
