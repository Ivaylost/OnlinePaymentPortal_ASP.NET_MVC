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
    public class AddTransactionAsync_Should
    {
        [TestMethod]
        public async Task Update_Balance()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Update_Balance));
            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.Accounts.Add(TestUtils.receiverAccount);
                arrangeContext.Accounts.Add(TestUtils.senderAccount);
                arrangeContext.TransactionTypes.Add(TestUtils.typeFix);
                await arrangeContext.SaveChangesAsync();

                var sut = new UserService(arrangeContext, null, null, null, null, null, null, null);
                var result = await sut.AddTransactionAsync(TestUtils.senderAccount.Id, TestUtils.receiverAccount.Id, "description", 777);
                Assert.IsInstanceOfType(result, typeof(Transaction));
                Assert.AreEqual(result.Amount, 777);
                Assert.AreEqual(result.RecieverAccount.Id, TestUtils.receiverAccount.Id);
                Assert.AreEqual(result.SenderAccount.Id, TestUtils.senderAccount.Id);
            }
        }
    }
}
