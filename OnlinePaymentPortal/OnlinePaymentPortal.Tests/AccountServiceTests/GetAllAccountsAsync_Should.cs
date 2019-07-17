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
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.AccountServiceTests
{
    [TestClass]
    public class GetAllAccountsAsync_Should
    {
        [TestMethod]
        public async Task Get_All_Accounts()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Get_All_Accounts));
            var accountDTOMapper = new Mock<IDtoMapper<Account, AccountDTO>>();

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var accountOne = TestUtils.account1;
                var accountTwo = TestUtils.account2;
                var balanceOne = TestUtils.balance1;
                var balanceTwo = TestUtils.balance2;
                var clientOne = TestUtils.client1;
                var clientTwo = TestUtils.client2;
                var currencyEUR = TestUtils.currencyEUR;

                arrangeContext.Accounts.Add(accountOne);
                arrangeContext.Accounts.Add(accountTwo);
                arrangeContext.Balances.Add(balanceOne);
                arrangeContext.Balances.Add(balanceTwo);
                arrangeContext.Clients.Add(clientOne);
                arrangeContext.Clients.Add(clientTwo);
                arrangeContext.Currencies.Add(currencyEUR);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new ApplicationDbContext(options))
            {
                //var accountOneExample = TestUtils.account1;
                //var accountTwoExample = TestUtils.account2;
                //var accountOneDTO = TestUtils.accountDTO1;
                //var accountTwoDTO = TestUtils.accountDTO2;
                var collectionsDTO = TestUtils.collectionsDTO;
                var allAccountsDTOMapperMock = new Mock<IDtoMapper<IReadOnlyCollection<Account>, CollectionsDTO>>();
                allAccountsDTOMapperMock.Setup(x => x.MapFrom(It.IsAny<List<Account>>())).Returns(collectionsDTO);
                var sut = new AccountService(assertContext, null, null, allAccountsDTOMapperMock.Object, null,null);
                var result = await sut.GetAllAccountsAsync();

                Assert.IsInstanceOfType(result, typeof(CollectionsDTO));
                Assert.IsInstanceOfType(result.Accounts, typeof(List<AccountDTO>));
                Assert.AreEqual(result.Accounts.Count, 2);
            }
        }
    }
}
