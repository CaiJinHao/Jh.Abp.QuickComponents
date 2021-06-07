using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.EntityFrameworkCore.DmExtensions
{
    public static class CommonFilterExpression
    {
        public static Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>(
            bool IsSoftDeleteFilterEnabled,
            bool IsMultiTenantFilterEnabled,
            Guid? CurrentTenantId,
            Func<Expression<Func<TEntity, bool>>, Expression<Func<TEntity, bool>>, Expression<Func<TEntity, bool>>> CombineExpressions)
        {
            Expression<Func<TEntity, bool>> expression = null;

            //WHERE (@__ef_filter__p_0 = CAST(1 AS bit)) OR ([s].[IsDeleted] <> CAST(1 AS bit))
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)) && IsSoftDeleteFilterEnabled)
            {
                expression = e => Convert.ToInt32(EF.Property<bool>(e, "IsDeleted")) == 0;
            }

            if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)) && IsMultiTenantFilterEnabled)
            {
                Expression<Func<TEntity, bool>> multiTenantFilter = e => EF.Property<Guid>(e, "TenantId") == CurrentTenantId;
                expression = expression == null ? multiTenantFilter : CombineExpressions(expression, multiTenantFilter);
            }

            return expression;
        }
    }
}
