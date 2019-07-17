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

namespace OnlinePaymentPortal.Tests.DTOMappersTests
{
    [TestClass]
    public class AccountDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_AccountDTO()
        {
                var currency = TestUtils.currencyFull;
                var balance = TestUtils.balanceFull;
                var accountExample = TestUtils.accountFull;
                var client = TestUtils.client1;
                var result = new AccountDTOMapper().MapFrom(accountExample);

                Assert.IsInstanceOfType(result, typeof(AccountDTO));
                Assert.AreEqual(result.Id, accountExample.Id);
                Assert.AreEqual(result.AccountNumber, accountExample.AccountNumber);
                Assert.AreEqual(result.NickName, accountExample.Nickname);
                Assert.AreEqual(result.ClientId, accountExample.ClientId);
                Assert.AreEqual(result.BalanceValue, balance.Value);
                Assert.AreEqual(result.CurrencyName, currency.Name);
        }
    }
}
