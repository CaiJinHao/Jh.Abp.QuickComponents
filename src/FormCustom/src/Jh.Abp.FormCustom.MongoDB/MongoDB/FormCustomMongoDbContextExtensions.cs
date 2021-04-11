using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Jh.Abp.FormCustom.MongoDB
{
    public static class FormCustomMongoDbContextExtensions
    {
        public static void ConfigureFormCustom(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new FormCustomMongoModelBuilderConfigurationOptions(
                FormCustomDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}