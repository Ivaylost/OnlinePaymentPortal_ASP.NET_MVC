using OnlinePaymentPortal.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.DateAccess.Contracts
{
    public interface IGenericRepository<T> where T: Entity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T FindById(Guid Id);
        ICollection<T> GetAll();
    }
}
