using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;

namespace Jh.Abp.QuickComponents.MiniProfiler
{
    public class MiniProfilerInterceptor : AbpInterceptor,ITransientDependency
    {
        public ILogger<MiniProfilerInterceptor> Logger { get; set; }
        public MiniProfilerInterceptor()
        {
            Logger = NullLogger<MiniProfilerInterceptor>.Instance;
        }
        public override async Task InterceptAsync(IAbpMethodInvocation invocation)
        {
            var profiler = StackExchange.Profiling.MiniProfiler.Current;
            using (profiler.Step(invocation.Method.Name))
            {
                // Do some work...
                await invocation.ProceedAsync();
            }
        }
    }
}
