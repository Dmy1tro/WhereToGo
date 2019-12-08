using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WhereToGoWebApi.DataBaseContext;
using WhereToGoWebApi.DataBaseContext.SeedData;
using WhereToGoWebApi.Models;

namespace WhereToGoWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EventDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                try
                {
                    SampleData.Proceed(context, userManager);
                }
                catch (Exception ex)
                {
                    var r = ex.Message;
                }

            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
