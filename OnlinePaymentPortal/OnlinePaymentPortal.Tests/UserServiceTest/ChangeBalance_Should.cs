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
    public class ChangeBalance_Should
    {
        [TestMethod]
        public async Task Update_Balance()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Update_Balance));
            var balanceDTOMapper = new Mock<IDtoMapper<Balance, BalanceDTO>>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Accounts.Add(TestUtils.receiverAccount);
                arrangeContext.Currencies.Add(TestUtils.currencyFull);
                arrangeContext.Balances.Add(TestUtils.balanceToUpdate);
                await arrangeContext.SaveChangesAsync();

                var sut = new UserService(arrangeContext, null, null, null, null, null, null, balanceDTOMapper.Object);
                var result = await sut.ChangeBalance(TestUtils.receiverAccount.Id, 100);
                balanceDTOMapper.Verify(x => x.MapFrom(It.Is<Balance>(xx => xx.Value == 100)));
            }
        }
    }
}
