using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OnlineLibrary.Data.Extensions;

namespace OnlineLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().CreateRoles().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
