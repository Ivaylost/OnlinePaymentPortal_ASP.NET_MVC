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

namespace OnlinePaymentPortal.Tests.CurrencyServiceTest
{
    [TestClass]
    public class GetAllCurrencyAsync_Should
    {
        [TestMethod]
        public async Task Return_All_Currency()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_All_Currency));
            

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Currencies.Add(TestUtils.currencyFull);
                await arrangeContext.SaveChangesAsync();

                var allCurrenciesDTOMapperTOMapper = new Mock<IDtoMapper<IReadOnlyCollection<Currency>, CollectionsDTO>>();
                var sut = new CurrencyService(arrangeContext, allCurrenciesDTOMapperTOMapper.Object);
                var result = await sut.GetAllCurrenciesAsync();
                allCurrenciesDTOMapperTOMapper.Verify(x => x.MapFrom(It.Is<List<Currency>>(xx => xx.Count() == 1)));
            }
        }
    }
}
