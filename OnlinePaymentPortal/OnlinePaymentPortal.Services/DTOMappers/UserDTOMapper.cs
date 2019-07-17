using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public class UserDTOMapper:IDtoMapper<User, UserDTO>
    {
        public UserDTO MapFrom(User entity)
        {
            return new UserDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                UserName = entity.UserName,
                Password = entity.PasswordHash,
                RoleId=entity.RoleId
            };
        }
    }
}
