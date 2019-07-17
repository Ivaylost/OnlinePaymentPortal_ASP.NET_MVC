using OnlinePaymentPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.DateAccess.Contracts
{
    public interface IUnitOfWork
    {
        void Save();

        void Dispose();

        void Rollback();
    }
}
