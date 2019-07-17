using Microsoft.EntityFrameworkCore;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext context;
        private readonly IDtoMapper<Client, ClientDTO> clientDTOMapper;
        private readonly IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO> allClientsDTOMapper;

        public ClientService(ApplicationDbContext context,
             IDtoMapper<Client, ClientDTO> clientDTOMapper,
             IDtoMapper<IReadOnlyCollection<Client>, CollectionsDTO> allClientsDTOMapper)
        {
            this.context = context ;
            this.clientDTOMapper = clientDTOMapper ;
            this.allClientsDTOMapper = allClientsDTOMapper;
        }

        //public Client GetClientByName(string name)
        //{
        //    return this.context.Clients.FirstOrDefault(c => c.Name == name);
        //}

        //public async Task<CollectionsDTO> GetAllClients()
        //{
        //    var clients= await this.context.Clients.ToListAsync();

        //   return this.allClientsDTOMapper.MapFrom(clients);

        //}
        //public async Task<IReadOnlyCollection<Client>> GetTopClients()
        //{
        //    return await this.context.Clients
        //            .OrderByDescending(c => c.CreatedOn)
        //            .Take(5)
        //            .ToListAsync();
        //}

        public async Task<CollectionsDTO> GetAllClientsAsync(int currentPage)
        {
            List<Client> clients;

            if (currentPage == 1)
            {
                clients = await this.context
                    .Clients
                    .OrderByDescending(c => c.CreatedOn)
                    .Take(5)
                    .ToListAsync();

            }
            else
            {
                clients = await this.context
                    .Clients
                    .OrderByDescending(c => c.CreatedOn)
                    .Skip((currentPage - 1) * 5)
                    .Take(5)
                    .ToListAsync();
            }

            return this.allClientsDTOMapper.MapFrom(clients);

        }

        public async Task<int> GetPageCount()
        {
            var count = await this.context.Clients.CountAsync();
            int pages = 0;

            if (count % 5 == 0)
            {
                pages = (count / 5);
            }
            else
            {
                pages = (count / 5) + 1;
            }
            return pages;
        }

        //public async Task<Client> FindClientByNameAsync(string name)
        //{
        //    return await this.context.Clients.FirstOrDefaultAsync(c => c.Name == name);
        //}

        public bool IsContains(string name)
        {
            return this.context.Clients.Any(p => p.Name == name);
        }

        public async Task<ClientDTO> CreateClientAsync(string name)
        {
            if (name==null)
            {
                throw new ArgumentNullException("Cannot be null");
            }

            if (this.context.Clients.Any(c=>c.Name==name))
            {
                throw new ArgumentException("Argument");
            }


            var client = new Client()
            {
                Name = name,
                CreatedOn = DateTime.Now
            };
            await this.context.Clients.AddAsync(client);
            await this.context.SaveChangesAsync();

            var clientDTO = this.clientDTOMapper.MapFrom(client);

            return clientDTO;
        }
    }
}
