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

namespace OnlinePaymentPortal.Tests.HashServiceTest
{
    [TestClass]
    public class HashService_Should
    {
        [TestMethod]
        public void Reurn_True_If_Password_Valid()
        {
            var password = "Admin123@";
            var hashedPassword = "aZKX26tFSB2qQLQoYsvQyA5ycir34TXBF8zKSwT2NtVWv5+a";

            var result = new HashService().IsPasswordValid(password, hashedPassword);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Reurn_False_If_Password_Not_Valid()
        {
            var password = "Admin123@";
            var hashedPassword = "gnkZNr59J7nQy+LjHjYkOqMm+2H64RX7Oiwyek8RQBiTVzyC";

            var result = new HashService().IsPasswordValid(password, hashedPassword);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void Generate_16byte_Salt()
        {
            var result = new HashService().GenerateSalt();
            Assert.AreEqual(result.Length, 16);
        }
    }
}
