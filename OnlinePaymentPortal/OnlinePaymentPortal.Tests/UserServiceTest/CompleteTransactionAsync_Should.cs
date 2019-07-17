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
    public class CompleteTransactionAsync_Should
    {
        [TestMethod]
        public void Complete_Transaction()
        {
            var options = DatabaseOrganisation.GetOptions(nameof(Complete_Transaction));
            var transactionDTOMapperMock = new Mock<IDtoMapper<Transaction, TransactionDTO>> ();
            var balanceDTOMapper = new Mock<IDtoMapper<Balance, BalanceDTO>> ();


            using (var arrangeContext = new ApplicationDbContext(options))
            {
                var senderAccountBalance = TestUtils.balanceToCheck;
                var receiverAccountBalanve = TestUtils.balanceToUpdate;
                arrangeContext.Currencies.Add(TestUtils.currencyFull);
                arrangeContext.Accounts.Add(TestUtils.senderAccount);
                arrangeContext.Accounts.Add(TestUtils.receiverAccount);
                arrangeContext.Balances.Add(senderAccountBalance);
                arrangeContext.Balances.Add(receiverAccountBalanve);
                arrangeContext.TransactionTypes.Add(TestUtils.typeNew);
                arrangeContext.Transactions.Add(TestUtils.transactionUserService);
                arrangeContext.SaveChangesAsync();

                var sut = new UserService(arrangeContext, null, null, null, null, null, transactionDTOMapperMock.Object, balanceDTOMapper.Object);
                var result = sut.CompleteTransactionAsync(TestUtils.transactionUserService.Id);
                Assert.AreEqual(senderAccountBalance.Value, 9000);
            }
        }

        //[TestMethod]
        //public void Throw_Exeption_If_No_Money()
        //{
        //    var options = DatabaseOrganisation.GetOptions(nameof(Throw_Exeption_If_No_Money));
        //    var transactionDTOMapperMock = new Mock<IDtoMapper<Transaction, TransactionDTO>>();
        //    var balanceDTOMapper = new Mock<IDtoMapper<Balance, BalanceDTO>>();


        //    using (var arrangeContext = new ApplicationDbContext(options))
        //    {
        //        var senderAccountBalance = TestUtils.balanceToCheck;
        //        var receiverAccountBalanve = TestUtils.balanceToUpdate;
        //        arrangeContext.Currencies.Add(TestUtils.currencyFull);
        //        arrangeContext.Accounts.Add(TestUtils.senderAccount);
        //        arrangeContext.Accounts.Add(TestUtils.receiverAccount);
        //        arrangeContext.Balances.Add(senderAccountBalance);
        //        arrangeContext.Balances.Add(receiverAccountBalanve);
        //        arrangeContext.TransactionTypes.Add(TestUtils.typeNew);
        //        var checkedAmout = TestUtils.transactionUserService;
        //        checkedAmout.Amount = 20000;
        //        arrangeContext.Transactions.Add(checkedAmout);
        //        arrangeContext.SaveChangesAsync();

        //        var sut = new UserService(arrangeContext, null, null, null, null, null, transactionDTOMapperMock.Object, balanceDTOMapper.Object);
                
        //        var ex = Assert.ThrowsException<ArgumentException>(() =>
        //        sut.CompleteTransactionAsync(TestUtils.transactionUserService.Id));
        //        Assert.AreEqual(ex.Message, "Not enough amount in balance");
        //    }
        //}
    }
}
