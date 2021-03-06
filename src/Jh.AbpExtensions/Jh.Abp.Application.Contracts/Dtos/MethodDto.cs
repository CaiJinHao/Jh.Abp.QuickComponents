﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Jh.Abp.Application.Contracts.Extensions
{
    /// <summary>
    /// 方法输入参数Dto
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public  class MethodDto<TEntity>
    {
        public Action<TEntity> CreateOrUpdateEntityAction;

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> QueryAction;
    }
}
