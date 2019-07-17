using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class RoleDTOMapper : IDtoMapper<Role, RoleDTO>
    {
        public RoleDTO MapFrom(Role entity)
        {
            return new RoleDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}


