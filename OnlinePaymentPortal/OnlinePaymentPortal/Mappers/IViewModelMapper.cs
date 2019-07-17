using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlinePaymentPortal.Models;

namespace OnlinePaymentPortal.Mappers
{
    public interface IViewModelMapper<TEntity, TViewModel>
    {
        TViewModel MapFrom(TEntity entity);
        //AccountDashViewModel MapFrom();
    }
}
