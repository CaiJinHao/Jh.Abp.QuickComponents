using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Application.Contracts.Dtos
{
    public interface IFullRetrieveDto<TEntity> : IMethodDto<TEntity>,IRetrieveDelete
    {

    }
}
