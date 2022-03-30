using EmployeeApi.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            seedEmployeeData(host);
            host.Run();
        }

        private static void seedEmployeeData(IHost host)
        {
            using(var scope=host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var empContext = services.GetRequiredService<EmployeeContext>();
                EmployeeContextSeed.SeedAsync(empContext);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                .UseStartup<Startup>()
                .UseUrls("http://*:8080"));
        }
    }
}
