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
    public class GetAccountTransactionsAsync_Should
    {
        [TestMethod]
        public async Task Add_Account_To_User()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Add_Account_To_User));
            var alltransactionsDTOMapper = new Mock<IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO>>();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                arrangeContext.TransactionTypes.Add(TestUtils.type);
                arrangeContext.Clients.Add(TestUtils.clientSenderAccount);
                arrangeContext.Clients.Add(TestUtils.clientReceiverAccount);
                arrangeContext.Accounts.Add(TestUtils.senderAccount);
                arrangeContext.Accounts.Add(TestUtils.receiverAccount);
                arrangeContext.Transactions.Add(TestUtils.transactionUserService);
                await arrangeContext.SaveChangesAsync();

                var sut = new UserService(arrangeContext, null, null, null, alltransactionsDTOMapper.Object, null, null, null);
                var result = await sut.GetAccountTransactionsAsync(TestUtils.receiverAccount.Id);
                alltransactionsDTOMapper.Verify(x => x.MapFrom(It.IsAny<List<Transaction>>()));
            }
        }
    }
}
