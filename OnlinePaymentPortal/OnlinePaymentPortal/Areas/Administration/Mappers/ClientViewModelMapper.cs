using OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces;
using OnlinePaymentPortal.Areas.Administration.Models;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Mappers
{
    public class ClientViewModelMapper : IViewModelMapper<ClientDTO, ClientViewModel>
    {
        public ClientViewModel MapFrom(ClientDTO entity)
     => new ClientViewModel
     {
         Id = entity.Id,
         Name = entity.Name
     };
    }
}

