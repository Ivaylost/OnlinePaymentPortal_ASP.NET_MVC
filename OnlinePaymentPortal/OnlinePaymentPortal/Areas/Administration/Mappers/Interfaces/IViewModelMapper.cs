using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces
{
    public interface IViewModelMapper<TEntity, TViewModel>
    {
        TViewModel MapFrom(TEntity entity);
    }
}
