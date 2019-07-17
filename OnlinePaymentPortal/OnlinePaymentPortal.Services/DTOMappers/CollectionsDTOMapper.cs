using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class CollectionsDTOMapper : 
        IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO>,
        IDtoMapper<IReadOnlyCollection<Banner>, CollectionsDTO>,
        IDtoMapper<IReadOnlyCollection<Account>, CollectionsDTO>,
        IDtoMapper<IReadOnlyCollection<User>, CollectionsDTO>,
        IDtoMapper<IReadOnlyCollection<Transaction>, CollectionsDTO>,
        IDtoMapper<IReadOnlyCollection<Currency>, CollectionsDTO>
    {
        private readonly IDtoMapper<Client, ClientDTO> clientMapper;
        private readonly IDtoMapper<Banner, BannerDTO> bannerMapper;
        private readonly IDtoMapper<Account, AccountDTO> accountMapper;
        private readonly IDtoMapper<User, UserDTO> userMapper;
        private readonly IDtoMapper<Transaction, TransactionDTO> transactionMapper;
        private readonly IDtoMapper<Currency, CurrencyDTO> currencyMapper;

        public CollectionsDTOMapper(IDtoMapper<Client, ClientDTO> clientMapper,
            IDtoMapper<Banner, BannerDTO> bannerMapper,
            IDtoMapper<Account, AccountDTO> accountMapper,
            IDtoMapper<User, UserDTO> userMapper,
            IDtoMapper<Transaction, TransactionDTO> transactionMapper,
            IDtoMapper<Currency, CurrencyDTO> currencyMapper)
        {
            this.clientMapper = clientMapper;
            this.bannerMapper = bannerMapper;
            this.accountMapper = accountMapper;
            this.userMapper = userMapper;
            this.transactionMapper = transactionMapper;
            this.currencyMapper = currencyMapper;
        }

        public CollectionsDTO MapFrom(IReadOnlyCollection<Client> entity)
        {
            return new CollectionsDTO
            {
                Clients = entity.Select(this.clientMapper.MapFrom).ToList(),
            };
        }

        public CollectionsDTO MapFrom(IReadOnlyCollection<Banner> entity)
        {
            return new CollectionsDTO
            {
                Banners = entity.Select(this.bannerMapper.MapFrom).ToList(),
            };
        }

        public CollectionsDTO MapFrom(IReadOnlyCollection<Account> entity)
        {
            return new CollectionsDTO
            {
                Accounts = entity.Select(this.accountMapper.MapFrom).ToList(),
            };
        }

        public CollectionsDTO MapFrom(IReadOnlyCollection<User> entity)
        {
            return new CollectionsDTO
            {
                Users = entity.Select(this.userMapper.MapFrom).ToList(),
            };
        }

        public CollectionsDTO MapFrom(IReadOnlyCollection<Transaction> entity)
        {
            return new CollectionsDTO
            {
                Transactions = entity.Select(this.transactionMapper.MapFrom).ToList(),
            };
        }

        public CollectionsDTO MapFrom(IReadOnlyCollection<Currency> entity)
        {
            return new CollectionsDTO
            {
                Currencies = entity.Select(this.currencyMapper.MapFrom).ToList(),
            };
        }
    }
}
