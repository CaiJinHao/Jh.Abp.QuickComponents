using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace Jh.Abp.MenuManagement.HttpApi.Client.ConsoleTestApp
{
    public class ConsoleTestAppHostedService : IHostedService
    {
        public  Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<MenuManagementConsoleApiClientModule>())
            {
                application.Initialize();

                //var demo = application.ServiceProvider.GetRequiredService<ClientDemoService>();
                //await demo.RunAsync();

                application.Shutdown();
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
