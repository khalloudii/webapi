using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //var host = CreateHostBuilder(args).Build();
            //using(var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    //var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //    try
            //    {
            //        //var context = services.GetRequiredService<StoreContext>();
            //        //await context.Database.MigrateAsync();
            //        //

            //        //var userManager = services.GetRequiredService<UserManager<AppUser>>();
            //        //var identityContext = services.GetRequiredService<AppIdentityDbContext>();
            //        //await identityContext.Database.MigrateAsync();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}

            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}