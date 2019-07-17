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
    public class CurrencyDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_CurrencyDTO()
        {
            var currency = TestUtils.currencyFull;
            var result = new CurrencyDTOMapper().MapFrom(currency);

            Assert.IsInstanceOfType(result, typeof(CurrencyDTO));
            Assert.AreEqual(result.Id, currency.Id);
            Assert.AreEqual(result.Name, currency.Name);
            Assert.AreEqual(result.Sign, currency.Sign);
            Assert.AreEqual(result.Abbr, currency.Abbr);
            Assert.AreEqual(result.IsPrefixed, currency.IsPrefixed);
        }
    }
}
