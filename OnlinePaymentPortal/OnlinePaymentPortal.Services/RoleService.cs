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
    public class RoleService:IRoleService
    {
        private readonly ApplicationDbContext context;
        private readonly IDtoMapper<Role, RoleDTO> roleDTOMapper;

        public RoleService(ApplicationDbContext context,
                        IDtoMapper<Role, RoleDTO> roleDTOMapper)
        {
            this.context = context;
            this.roleDTOMapper = roleDTOMapper;
        }

        public async Task<RoleDTO> GetRoleByIdAsync(Guid Id)
        {
            var role= await this.context.Roles.FirstOrDefaultAsync(r => r.Id == Id);
            var roleDTO = this.roleDTOMapper.MapFrom(role);
            return roleDTO;
        }
    }
}
