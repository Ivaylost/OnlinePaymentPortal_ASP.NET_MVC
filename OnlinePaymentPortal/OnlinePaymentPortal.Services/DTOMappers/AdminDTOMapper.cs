using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class AdminDTOMapper : IDtoMapper<Admin, AdminDTO>
    {
        public AdminDTO MapFrom(Admin entity)
        {
            return new AdminDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                UserName = entity.UserName,
                Password = entity.PasswordHash,
                RoleId = entity.RoleId
            };
        }
    }
}
