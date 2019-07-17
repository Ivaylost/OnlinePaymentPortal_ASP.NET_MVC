using Microsoft.EntityFrameworkCore;
using OnlinePaymentPortal.Data;
using OnlinePaymentPortal.Data.Abstractions;
using OnlinePaymentPortal.DateAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlinePaymentPortal.DateAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            context.Add(entity);
        }

        public void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            context.Remove(entity);
        }

        public T FindById(Guid Id)
        {
           return dbSet.FirstOrDefault(x => x.Id == Id);
        }

        public ICollection<T> GetAll()
        {
            return dbSet.ToList();
        }   

        public void Update(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            context.Update(entity);
        }
    }
}
