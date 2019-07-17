using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Services.Interfaces
{
    public interface IAdminService
    {
        Task<AdminDTO> FindAdminAsync(string username, string password);
    }
}
