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

namespace OnlinePaymentPortal.Tests.ClientServiceTest
{
    [TestClass]
    public class GetAllClientsAsync_Should
    {
        [TestMethod]
        public async Task Get_All_Client_Per_Page1()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Get_All_Client_Per_Page1));
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 11; i++)
                {
                    var guid = Guid.NewGuid();
                    var clientNew = new Client()
                    {
                        Id = guid,
                        Name = guid.ToString(),
                        CreatedOn = DateTime.Now,
                    };
                    arrangeContext.Clients.Add(clientNew);
                }

                await arrangeContext.SaveChangesAsync();
                var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
                var allClientDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>>();
                var clientDto = new ClientDTO();
                var sut = new ClientService(arrangeContext, clientDTOMapperMock.Object, allClientDTOMapperMock.Object);
                var client = await sut.GetAllClientsAsync(1);
                allClientDTOMapperMock.Verify(x => x.MapFrom(It.Is<List<Client>>(xx => xx.Count() == 5)));
            }
        }

        [TestMethod]
        public async Task Get_All_Client_Per_Page2()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Get_All_Client_Per_Page2));
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                for (int i = 0; i < 11; i++)
                {
                    var guid = Guid.NewGuid();
                    var clientNew = new Client()
                    {
                        Id = guid,
                        Name = guid.ToString(),
                        CreatedOn = DateTime.Now,
                    };
                    arrangeContext.Clients.Add(clientNew);
                }

                await arrangeContext.SaveChangesAsync();
                var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
                var allClientDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>>();
                var clientDto = new ClientDTO();
                var sut = new ClientService(arrangeContext, clientDTOMapperMock.Object, allClientDTOMapperMock.Object);
                var client = await sut.GetAllClientsAsync(3);
                allClientDTOMapperMock.Verify(x => x.MapFrom(It.Is<List<Client>>(xx => xx.Count() == 1)));
            }
        }
    }
}
