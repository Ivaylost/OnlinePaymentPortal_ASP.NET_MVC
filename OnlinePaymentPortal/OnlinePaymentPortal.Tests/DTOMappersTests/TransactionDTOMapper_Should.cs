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
    public class TransactionDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_TransactionDTO()
        {
            var transaction = TestUtils.transactionFull;
            var result = new TransactionDTOMapper().MapFrom(transaction);

            Assert.IsInstanceOfType(result, typeof(TransactionDTO));
            Assert.AreEqual(result.Id, transaction.Id);
            Assert.AreEqual(result.PayerAccount, transaction.SenderAccount.AccountNumber);
            Assert.AreEqual(result.PayerClientName, transaction.SenderAccount.Client.Name);
            Assert.AreEqual(result.PayeeAccount, transaction.RecieverAccount.AccountNumber);
            Assert.AreEqual(result.PayeeClientName, transaction.RecieverAccount.Client.Name);
            Assert.AreEqual(result.PaymentDescription, transaction.Description);
            Assert.AreEqual(result.PaymentTimestamp, transaction.Timestamp);
            Assert.AreEqual(result.PaymentStatus, transaction.Type.Name);
            Assert.AreEqual(result.Ammount, transaction.Amount);
        }
    }
}
