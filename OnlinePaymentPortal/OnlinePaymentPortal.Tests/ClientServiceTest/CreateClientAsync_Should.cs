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
    public class CreateClientAsync_Should
    {
        [TestMethod]
        public async Task Throw_Exeption_If_Client_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Throw_Exeption_If_Client_Exist));
            var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
            var allClientDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>>();
            var clientDto = new ClientDTO();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Clients.Add(TestUtils.client1);
                arrangeContext.Clients.Add(TestUtils.client2);
                await arrangeContext.SaveChangesAsync();
                var sut = new ClientService(arrangeContext, clientDTOMapperMock.Object, allClientDTOMapperMock.Object);
                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await sut.CreateClientAsync("Name1"));
                Assert.AreEqual("Argument", ex.Message);
            }
        }

        [TestMethod]
        public async Task Throw_Exeption_If_ClientName_Null()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Throw_Exeption_If_Client_Exist));
            var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
            var allClientDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>>();
            var clientDto = new ClientDTO();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Clients.Add(TestUtils.client1);
                arrangeContext.Clients.Add(TestUtils.client2);
                await arrangeContext.SaveChangesAsync();
                string name = null;

                var sut = new ClientService(arrangeContext, clientDTOMapperMock.Object, allClientDTOMapperMock.Object);
                var ex = await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
                await sut.CreateClientAsync(name));
                Assert.IsTrue(ex.Message.Contains("Cannot be null"));
            }
        }

        [TestMethod]
        public async Task Create_Client()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Create_Client));
            var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
            var allClientDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>>();
            var clientDto = new ClientDTO();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Clients.Add(TestUtils.client1);
                arrangeContext.Clients.Add(TestUtils.client2);
                await arrangeContext.SaveChangesAsync();
                var sut = new ClientService(arrangeContext, clientDTOMapperMock.Object, allClientDTOMapperMock.Object);
                var name = "ClientsName";
                var result = sut.CreateClientAsync(name);
                clientDTOMapperMock.Verify(x => x.MapFrom(It.Is<Client>(xx => xx.Name == name)));
            }
        }
    }
}
