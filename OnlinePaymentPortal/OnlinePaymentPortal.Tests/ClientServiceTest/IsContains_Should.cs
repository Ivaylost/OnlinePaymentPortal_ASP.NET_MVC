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
    public class IsContains_Should
    {
        [TestMethod]
        public void Return_True_If_Client_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_True_If_Client_Exist));
            var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
            var allClientDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>>();
            var clientDto = new ClientDTO();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Clients.Add(TestUtils.client1);
                arrangeContext.Clients.Add(TestUtils.client2);
                arrangeContext.SaveChangesAsync();
                var sut = new ClientService(arrangeContext, clientDTOMapperMock.Object, allClientDTOMapperMock.Object);
                var result = sut.IsContains("Name1");
                Assert.AreEqual(result, true);
            }
        }

        [TestMethod]
        public void Return_False_If_Client_Do_Not_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_False_If_Client_Do_Not_Exist));
            var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
            var allClientDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>>();
            var clientDto = new ClientDTO();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Clients.Add(TestUtils.client1);
                arrangeContext.Clients.Add(TestUtils.client2);
                arrangeContext.SaveChangesAsync();
                var sut = new ClientService(arrangeContext, clientDTOMapperMock.Object, allClientDTOMapperMock.Object);
                var result = sut.IsContains("anotherName");
                Assert.AreEqual(result, false);
            }
        }
    }
}
