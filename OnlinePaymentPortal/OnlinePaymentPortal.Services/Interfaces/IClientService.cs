using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services.Interfaces
{
    public interface IClientService
    {
        Task<CollectionsDTO> GetAllClientsAsync(int currentPage);
        Task<ClientDTO> CreateClientAsync(string name);
        //TODO?
        //Task<Client> FindClientByNameAsync(string name);
        Task<int> GetPageCount();
        //Task<CollectionsDTO> GetAllClients();

        bool IsContains(string name);

        //TODO:
        //Client GetClientByName(string name);

    }
}
