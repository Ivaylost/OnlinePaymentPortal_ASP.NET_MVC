using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Tests.Utilis
{
    public static class TestUtils
    {
        //public static DbContextOptions<ApplicationDbContext> GetOptions()
        //{
        //    var provider = new ServiceCollection()
        //        .AddEntityFrameworkInMemoryDatabase()
        //        .BuildServiceProvider();
        //    return new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .UseInternalServiceProvider(provider)
        //        .Options;
        //}

        public static Account account1 = new Account()
        {
            Id = Guid.Parse("8b48bffc-4119-461f-804c-668b71cdaae5"),
            AccountNumber = "10000000001",
            Nickname = "Nickname",
            //Client = client1,
            //Balances = new List<Balance>() { balance1 },
        };

        public static Account account2 = new Account()
        {
            Id = Guid.Parse("feac5bcd-117f-40b9-a7d8-7a5775ea6b46"),
            AccountNumber = "10000000002",
            Nickname = "Nickname2",
        };

        public static Balance balance1 = new Balance()
        {
            Id = Guid.Parse("d1981a3e-c60c-43fb-8418-170745a46155"),
            Value = 10000,
            AccountId = Guid.Parse("8b48bffc-4119-461f-804c-668b71cdaae5"),
            CurrencyId = Guid.Parse("5693189f-3823-4565-b03f-e80b28d03527"),
        };

        public static Balance balance2 = new Balance()
        {
            Id = Guid.Parse("618b7d84-2c44-4bbb-8ef1-354177f9c0e5"),
            Value = 5000,
            AccountId = Guid.Parse("feac5bcd-117f-40b9-a7d8-7a5775ea6b46"),
        };

        public static Client client1 = new Client
        {
            Id = Guid.Parse("7cd344b1-7b51-4c78-a20a-31065f4a258f"),
            Name = "Name1",
            CreatedOn = DateTime.MinValue,
            Accounts = new List<Account>() {account1},
        };

        public static Client clientNoAccount = new Client
        {
            Id = Guid.Parse("89c5fbef-ab6c-4864-801f-bd89cfeddf1a"),
            Name = "Name1",
            CreatedOn = DateTime.MinValue,
        };

        public static Client client2 = new Client
        {
            Id = Guid.Parse("a6a0556e-b3c6-443d-bd0e-333539078a4b"),
            Name = "Name2",
            CreatedOn = DateTime.MinValue,
            Accounts = new List<Account>() { account2 },
        };

        public static Banner banner1 = new Banner
        {
            Id = Guid.Parse("cd4ba4c3-08dd-468b-8918-5427dd9fc4c3"),
            ImagePath = "path",
            StartDate = DateTime.Parse("01.01.2018"),
            EndDate = DateTime.Parse("01.01.2020"),
        };

        public static Banner banner2 = new Banner
        {
            Id = Guid.Parse("f204fdc7-a125-43a2-94f7-356aff543f87"),
            StartDate = DateTime.Parse("01.01.2018"),
            EndDate = DateTime.Parse("01.01.2020"),
        };

        

        public static BannerDTO bannerDTO1 = new BannerDTO
        {
            Id = Guid.Parse("3baff448-7c4f-4c86-b829-83c6e42ebd84"),
        };

        public static BannerDTO bannerDTO2 = new BannerDTO
        {
            Id = Guid.Parse("3baff448-7c4f-4c86-b829-83c6e42ebd84"),
        };

        public static AccountDTO accountDTO1 = new AccountDTO()
        {
            Id = Guid.Parse("786d26b9-85b2-41d9-aeed-4e8bf58d09e1"),
            ClientId = Guid.Parse("7cd344b1-7b51-4c78-a20a-31065f4a258f"),
            CurrencyId = Guid.Parse("fe2800b2-28ee-4480-9c6d-392e7b5c419e"),
            AccountNumber = "10000000001",
            ClientName = "Name1",
            BalanceValue = 10000,
            NickName = "Nickname",
        };

        public static AccountDTO accountDTO2 = new AccountDTO()
        {
            Id = Guid.Parse("d25d0ad5-f9bc-46d8-b561-d5a0644e5e2c"),
            ClientId = Guid.Parse("a6a0556e-b3c6-443d-bd0e-333539078a4b"),
            AccountNumber = "10000000002",
            ClientName = "Name2",
            BalanceValue = 5000,
            NickName = "Nickname",
        };

        public static Currency currencyEUR = new Currency
        {
            Id = Guid.Parse("fe2800b2-28ee-4480-9c6d-392e7b5c419e"),
            Name = "EUR",
            //Balance = new List<Balance>() { balance1, balance2 },
        };

        public static CollectionsDTO collectionsDTO = new CollectionsDTO
        {
            Accounts = new List<AccountDTO>() { accountDTO1, accountDTO2 },
            Banners = new List<BannerDTO>() { bannerDTO1, bannerDTO2 },
        };

        public static Currency currencyFull = new Currency()
        {
            Id = Guid.Parse("fe2800b2-28ee-4480-9c6d-392e7b5c419e"),
            Name = "EUR",
            Sign = "$",
            Abbr = "euro",
            IsPrefixed = true,
        };

        public static Balance balanceFull = new Balance()
        {
            Id = Guid.Parse("d1981a3e-c60c-43fb-8418-170745a46155"),
            Value = 10000,
            AccountId = Guid.Parse("8b48bffc-4119-461f-804c-668b71cdaae5"),
            CurrencyId = Guid.Parse("fe2800b2-28ee-4480-9c6d-392e7b5c419e"),
            Currency = currencyFull,
        };

        public static Balance balanceFull1 = new Balance()
        {
            Id = Guid.Parse("c99e1ff7-9336-429f-b989-3ba4977f0cf9"),
            Value = 5000,
            AccountId = Guid.Parse("645e0f15-8df5-4819-9b20-e837755db064"),
            CurrencyId = Guid.Parse("fe2800b2-28ee-4480-9c6d-392e7b5c419e"),
            Currency = currencyFull,
        };

        public static Account accountFull = new Account()
        {
            Id = Guid.Parse("8b48bffc-4119-461f-804c-668b71cdaae5"),
            AccountNumber = "1111111111",
            Nickname = "Nickname",
            Client = TestUtils.client1,
            ClientId = TestUtils.client1.Id,
            Balances = new List<Balance>() { balanceFull },
        };

        public static Account accountFull1 = new Account()
        {
            Id = Guid.Parse("645e0f15-8df5-4819-9b20-e837755db064"),
            AccountNumber = "9999999999",
            Nickname = "NicknameOne",
            Client = TestUtils.client2,
            ClientId = TestUtils.client2.Id,
            Balances = new List<Balance>() { balanceFull1 },
        };


        public static Admin adminFull = new Admin()
        {
            Id = Guid.Parse("e93721db-da1a-4532-a38b-5f8a37ac7185"),
            Name = "Admin",
            UserName = "UserName",
            PasswordHash = "aZKX26tFSB2qQLQoYsvQyA5ycir34TXBF8zKSwT2NtVWv5+a",
            RoleId = Guid.Parse("4f6cf55b-6382-4a5c-bff7-41c13c0fbd8d"),
        };

        public static Role roleFull = new Role()
        {
            Id = Guid.Parse("249923cf-f1ed-4ef6-8297-05fbed4efedd"),
            Name = "user",
        };

        public static TransactionType type = new TransactionType()
        {
            Id = Guid.Parse("f934d231-c1f8-457f-8a02-ffa73eb64ccf"),
            Name = "Done",
        };

        public static Transaction transactionFull = new Transaction()
        {
            Id = Guid.Parse("64159dcc-4091-41dd-a8f0-25a38f356b87"),
            SenderAccountId = accountFull.Id,
            SenderAccount = accountFull,
            RecieverAccountId = accountFull1.Id,
            RecieverAccount = accountFull1,
            Description = "description",
            Amount = 7777,
            Timestamp = DateTime.Parse("01.5.2019"),
            TypeId = Guid.Parse("f934d231-c1f8-457f-8a02-ffa73eb64ccf"),
            Type = type,
        };

        public static User user = new User()
        {
            Id = Guid.Parse("f5eea00d-6e08-4c91-800b-ecc61604eb7b"),
            Name = "Name",
            UserName = "UserName",
            PasswordHash = "aZKX26tFSB2qQLQoYsvQyA5ycir34TXBF8zKSwT2NtVWv5+a",
            RoleId = Guid.Parse("249923cf-f1ed-4ef6-8297-05fbed4efedd"),
            Role = roleFull,
        };

        public static Client clientSenderAccount = new Client()
        {
            Id = Guid.Parse("d9331224-b189-419b-b7b4-5623a832d51c"),
            Name = "clientSA",
        };

        public static Client clientReceiverAccount = new Client()
        {
            Id = Guid.Parse("8fc12b59-78a4-45e8-aae0-d2003b6d1d21"),
            Name = "clientRA",
        };

        public static TransactionType typeFix = new TransactionType()
        {
            Id = Guid.Parse("7A66F826-8F0A-40C3-A085-3836EFE58DAE"),
            Name = "save",
        };

        public static Account senderAccount = new Account()
        {
            Id = Guid.Parse("604afd22-1371-48e0-a51d-a6c9c4db0664"),
            ClientId = clientSenderAccount.Id,
            Client = clientSenderAccount,
        };

        public static Account receiverAccount = new Account()
        {
            Id = Guid.Parse("d52f05d6-8a63-48bd-a0f3-abfec6ae0d39"),
            ClientId = clientReceiverAccount.Id,
            Client = clientReceiverAccount,
        };

        public static Transaction transactionUserService = new Transaction()
        {
            Id = Guid.Parse("dfcbb1d2-1d9a-4731-853d-b6633b6f9411"),
            SenderAccountId = senderAccount.Id,
            SenderAccount = senderAccount,
            RecieverAccountId = receiverAccount.Id,
            RecieverAccount = receiverAccount,
            Amount = 1000,
            TypeId = type.Id,
            Type = type,
        };

        public static Balance balanceToUpdate = new Balance()
        {
            Id = Guid.Parse("dfe9f19f-9614-4a6e-bd17-1035f11611e3"),
            Value = 10000,
            CurrencyId = currencyFull.Id,
            Currency = currencyFull,
            AccountId = receiverAccount.Id,
            Account = receiverAccount,
        };


        public static Balance balanceToCheck = new Balance()
        {
            Id = Guid.Parse("265cdcd5-46d3-4a4e-aa76-dc78aac76a30"),
            Value = 10000,
            CurrencyId = currencyFull.Id,
            Currency = currencyFull,
            AccountId = senderAccount.Id,
            Account = senderAccount,
        };

        public static TransactionType typeNew = new TransactionType()
        {
            Id = Guid.Parse("72a21942-3cc3-43b6-af02-e33c5dbbd272"),
            Name = "complete",
        };
    }
}
