using Jh.Abp.FormCustom.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jh.Abp.FormCustom
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(FormCustomEntityFrameworkCoreTestModule)
        )]
    public class FormCustomDomainTestModule : AbpModule
    {
        
    }
}
