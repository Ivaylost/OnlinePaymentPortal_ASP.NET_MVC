
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Configuration;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Tests.Utilis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.AccountServiceTests
{
    [TestClass]
    public class AddAccountAsync_Should
    {
        [TestMethod]
        public async Task Add_Correct_Account()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Add_Correct_Account));

            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Clients.Add(TestUtils.clientNoAccount);
                arrangeContext.Currencies.Add(TestUtils.currencyEUR);
                await arrangeContext.SaveChangesAsync();

                

                var accountOneDTO = new AccountDTO();
                var accountDTOMapperMock = new Mock<IDtoMapper<Account, AccountDTO>>();
                var configMock = new Mock<IConfiguration>();

                var sut = new AccountService(arrangeContext, accountDTOMapperMock.Object, null, null, null,configMock.Object);
                accountDTOMapperMock.Setup(x => x.MapFrom(It.IsAny<Account>())).Returns(accountOneDTO);
                configMock.Setup(m => m.GetSection("GlobalConstants:Balance").Value).Returns("10000");
                var account = await sut.AddAccountAsync(TestUtils.clientNoAccount.Id, TestUtils.currencyEUR.Id);
                
                Assert.IsInstanceOfType(account, typeof(AccountDTO));
                Assert.AreEqual(arrangeContext.Accounts.Count(), 1);
            }

            //using (var assertContext = new ApplicationDbContext(options))
            //{
            //    var accountOneDTO = TestUtils.accountDTO1;
            //    var accountDTOMapperMock = new Mock<IDtoMapper<Account, AccountDTO>>();
            //    accountDTOMapperMock.Setup(x => x.MapFrom(It.IsAny<Account>())).Returns(accountOneDTO);
            //    var sut = new AccountService(assertContext, accountDTOMapperMock.Object, null, null, null);
                
            //    var account = await sut.AddAccountAsync(TestUtils.clientNoAccount.Id, TestUtils.currencyEUR.Id);
            //    Assert.IsInstanceOfType(account, typeof(AccountDTO));
            //    Assert.AreEqual(assertContext.Accounts.Count(), 1);
            //}
        }
    }
}
