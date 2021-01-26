using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Application.Contracts.Extensions
{
    public abstract class UpdateActionBase<TEntity>
    {
        public Action<TEntity> UpdateEntityAction;
    }
}
