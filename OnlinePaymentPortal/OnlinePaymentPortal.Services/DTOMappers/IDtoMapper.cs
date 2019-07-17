using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePaymentPortal.Services.DTOMappers
{
    public interface IDtoMapper<TEntity, TDTO>
    {
        TDTO MapFrom(TEntity entity);
    }
}
