using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Tests.Utilis;
using System.Collections.Generic;

namespace OnlinePaymentPortal.Tests.DTOMappersTests
{
    [TestClass]
    public class CollectionsDTOMapper_Should
    {
        [TestMethod]
        public void Map_Correct_Client_CollectionDTO()
        {
            var client = TestUtils.client1;
            var client1 = TestUtils.client1;
            List<Client> collection = new List<Client>();
            collection.Add(client);
            collection.Add(client1);

            var clientMapper = new List<CollectionsDTO>();
            var clientDTOMapperMock = new Mock<IDtoMapper<Client, ClientDTO>>();
            var sut = new CollectionsDTOMapper(clientDTOMapperMock.Object, null, null, null, null, null);
            var result = sut.MapFrom(collection);
            Assert.IsInstanceOfType(result, typeof(CollectionsDTO));
            Assert.AreEqual(result.Clients.Count, 2);
        }

        [TestMethod]
        public void Map_Correct_Banner_CollectionDTO()
        {
            var banner = TestUtils.banner1;
            var banner1 = TestUtils.banner1;
            List<Banner> collection = new List<Banner>();
            collection.Add(banner);
            collection.Add(banner1);

            var bannerMapper = new List<CollectionsDTO>();
            var bannerDTOMapperMock = new Mock<IDtoMapper<Banner, BannerDTO>>();
            var sut = new CollectionsDTOMapper(null, bannerDTOMapperMock.Object, null, null, null, null);
            var result = sut.MapFrom(collection);
            Assert.IsInstanceOfType(result, typeof(CollectionsDTO));
            Assert.AreEqual(result.Banners.Count, 2);
        }

        [TestMethod]
        public void Map_Correct_Account_CollectionDTO()
        {
            var account = TestUtils.account1;
            var account1 = TestUtils.account2;
            List<Account> collection = new List<Account>();
            collection.Add(account);
            collection.Add(account1);

            var accountMapper = new List<CollectionsDTO>();
            var accountDTOMapperMock = new Mock<IDtoMapper<Account, AccountDTO>>();
            var sut = new CollectionsDTOMapper(null, null, accountDTOMapperMock.Object, null, null, null);
            var result = sut.MapFrom(collection);
            Assert.IsInstanceOfType(result, typeof(CollectionsDTO));
            Assert.AreEqual(result.Accounts.Count, 2);
        }

        [TestMethod]
        public void Map_Correct_User_CollectionDTO()
        {
            var user = TestUtils.user;
            var user1 = TestUtils.user;
            List<User> collection = new List<User>();
            collection.Add(user);
            collection.Add(user1);

            var usertMapper = new List<CollectionsDTO>();
            var userDTOMapperMock = new Mock<IDtoMapper<User, UserDTO>>();
            var sut = new CollectionsDTOMapper(null, null, null, userDTOMapperMock.Object, null, null);
            var result = sut.MapFrom(collection);
            Assert.IsInstanceOfType(result, typeof(CollectionsDTO));
            Assert.AreEqual(result.Users.Count, 2);
        }

        [TestMethod]
        public void Map_Correct_Transacrion_CollectionDTO()
        {
            var transaction = TestUtils.transactionFull;
            var transaction1 = TestUtils.transactionFull;
            List<Transaction> collection = new List<Transaction>();
            collection.Add(transaction);
            collection.Add(transaction1);

            var transactionMapper = new List<CollectionsDTO>();
            var transactionDTOMapperMock = new Mock<IDtoMapper<Transaction, TransactionDTO>>();
            var sut = new CollectionsDTOMapper(null, null, null, null, transactionDTOMapperMock.Object, null);
            var result = sut.MapFrom(collection);
            Assert.IsInstanceOfType(result, typeof(CollectionsDTO));
            Assert.AreEqual(result.Transactions.Count, 2);
        }

        [TestMethod]
        public void Map_Correct_Currency_CollectionDTO()
        {
            var currency = TestUtils.currencyEUR;
            var currency1 = TestUtils.currencyFull;
            List<Currency> collection = new List<Currency>();
            collection.Add(currency);
            collection.Add(currency1);

            var currencyMapper = new List<CollectionsDTO>();
            var currencynDTOMapperMock = new Mock<IDtoMapper<Currency, CurrencyDTO>>();
            var sut = new CollectionsDTOMapper(null, null, null, null, null, currencynDTOMapperMock.Object);
            var result = sut.MapFrom(collection);
            Assert.IsInstanceOfType(result, typeof(CollectionsDTO));
            Assert.AreEqual(result.Currencies.Count, 2);
        }
    }
}
