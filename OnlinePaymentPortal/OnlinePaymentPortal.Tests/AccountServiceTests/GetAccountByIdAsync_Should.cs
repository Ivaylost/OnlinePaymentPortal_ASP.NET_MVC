using Microsoft.EntityFrameworkCore;
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
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.AccountServiceTests
{
    [TestClass]
    public class GetAccountByIdAsync_Should
    {
        [TestMethod]
        public async Task Return_Account_If_Exist()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Return_Account_If_Exist));
            var accountDTOMapper = new Mock<IDtoMapper<Account, AccountDTO>>();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var accountOne = TestUtils.account1;
                var balanceOne = TestUtils.balance1;
                var clientOne = TestUtils.client1;
                arrangeContext.Accounts.Add(accountOne);
                arrangeContext.Balances.Add(balanceOne);
                arrangeContext.Clients.Add(clientOne);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                var accountOneExample = TestUtils.account1;
                var accountOneDTO = TestUtils.accountDTO1;
                var accountDTOMapperMock = new Mock<IDtoMapper<Account, AccountDTO>>();
                accountDTOMapperMock.Setup(x => x.MapFrom(It.Is<Account>(xx => xx.Id== accountOneExample.Id))).Returns(accountOneDTO);
                var sut = new AccountService(assertContext, accountDTOMapperMock.Object, null, null, null,null);
                var result = await sut.GetAccountByIdAsync(TestUtils.account1.Id);
                
                Assert.AreEqual(assertContext.Accounts.FirstAsync().Result.Nickname, accountOneExample.Nickname);
                Assert.AreEqual(assertContext.Accounts.FirstAsync().Result.Id, accountOneExample.Id);
                Assert.AreEqual(assertContext.Accounts.FirstAsync().Result.AccountNumber, accountOneExample.AccountNumber);
                Assert.AreEqual(assertContext.Accounts.FirstAsync().Result.ClientId, accountOneExample.ClientId);
            }
        }
    }
}
