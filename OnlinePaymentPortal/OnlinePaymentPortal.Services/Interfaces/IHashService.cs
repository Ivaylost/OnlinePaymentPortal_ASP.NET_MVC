using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.Interfaces
{
    public interface IHashService
    {
        bool IsPasswordValid(string enteredPassword, string base64Hash);

        string HashPassword(string password, byte[] salt);

        byte[] GenerateSalt();
    }
}
