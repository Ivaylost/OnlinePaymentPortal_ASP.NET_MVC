using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class ClientDTOMapper : IDtoMapper<Client, ClientDTO>
    {
        public ClientDTO MapFrom(Client entity)
        {
            return new ClientDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

    }
}
