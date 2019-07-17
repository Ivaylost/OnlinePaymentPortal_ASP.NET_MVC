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
    public class UserViewModelMapper : IViewModelMapper<UserDTO, UserViewModel>
    {
        public UserViewModel MapFrom(UserDTO entity)
        {
            return new UserViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                UserName = entity.UserName,
                Password = entity.Password,
            };
        }
    }
}
