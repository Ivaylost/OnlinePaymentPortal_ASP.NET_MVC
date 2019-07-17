using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Tests.Utilis;

namespace OnlinePaymentPortal.Tests.DTOMappersTests
{
    [TestClass]
    public class ClientDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_ClientDTO()
        {
            var client = TestUtils.client1;
            var result = new ClientDTOMapper().MapFrom(client);

            Assert.IsInstanceOfType(result, typeof(ClientDTO));
            Assert.AreEqual(result.Id, client.Id);
            Assert.AreEqual(result.Name, client.Name);
        }
    }
}
