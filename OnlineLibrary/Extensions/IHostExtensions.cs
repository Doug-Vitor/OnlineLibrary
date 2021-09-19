using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Data;
using System;

namespace OnlineLibrary.Extensions
{
    public static class IHostExtensions
    {
        public static IHost CreateRoles(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    SeedingServices.CreateRolesAsync(serviceProvider).Wait();
                }
                catch (Exception exception)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "Ocorreu um erro na criação dos perfis dos usuários.");
                }
            }

            return host;
        }
    }
}
