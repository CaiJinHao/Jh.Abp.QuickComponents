using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.DynamicProxy;

namespace Jh.Abp.QuickComponents.MiniProfiler
{
    public static class MiniProfilerInterceptorRegistrar
    {
        public static void RegisterIfNeeded(IOnServiceRegistredContext context)
        {
            //为所有继承接口的添加拦截器
            var AopTypeArray = new Type[] {
                typeof(IApplicationService), 
                typeof(IDomainService),
                typeof(IRepository)
            };
            if (((TypeInfo)context.ImplementationType).ImplementedInterfaces.Any(b => AopTypeArray.Contains(b)))
            {
                context.Interceptors.TryAdd<MiniProfilerInterceptor>();
            }
        }
    }
}
