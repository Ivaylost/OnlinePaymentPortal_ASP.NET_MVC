using Microsoft.EntityFrameworkCore;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOMappers;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services
{
    public class AdminService:IAdminService
    {
        private readonly ApplicationDbContext context;
        private readonly IDtoMapper<Admin, AdminDTO> adminDTOMapper;

        public AdminService(ApplicationDbContext context,
            IDtoMapper<Admin, AdminDTO> adminDTOMapper)
        {
            this.context = context;
            this.adminDTOMapper = adminDTOMapper;
        }
        
        public async Task<AdminDTO> FindAdminAsync(string username, string password)
        {
            var user = await this.context.Admins.FirstOrDefaultAsync(u => u.UserName == username);
            //TODO check against Hashed password
            if (user != null && user.PasswordHash == password)
            {
                var userDTO=this.adminDTOMapper.MapFrom(user);
                return userDTO;
            }

            return null;
        }

    }
}
