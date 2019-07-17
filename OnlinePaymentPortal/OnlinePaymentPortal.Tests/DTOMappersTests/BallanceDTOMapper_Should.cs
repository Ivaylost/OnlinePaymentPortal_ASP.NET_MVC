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
    public class BallanceDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_BalanceDTO()
        {
            var balance = TestUtils.balance1;
            var result = new BalanceDTOMapper().MapFrom(balance);

            Assert.IsInstanceOfType(result, typeof(BalanceDTO));
            Assert.AreEqual(result.Id, balance.Id);
            Assert.AreEqual(result.Value, balance.Value);
            Assert.AreEqual(result.CurrencyId, balance.CurrencyId);
            Assert.AreEqual(result.AccountId, balance.AccountId);
        }
    }
}
